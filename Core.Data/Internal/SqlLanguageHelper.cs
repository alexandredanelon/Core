using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Internal
{
    internal class SqlLanguageHelper
    {
        /// <summary>
        /// Make a Insert to 
        /// </summary>
        /// <param name="entity">E</param>
        /// <returns></returns>
        public static string Insert(DataModel entity, string targetTable)
        {
            var buildedInsert = string.Empty;
            var builder = new StringBuilder();
            var builderField = new StringBuilder();
            var builderFieldValue = new StringBuilder();
            var eType = entity.GetType();
            var entityAttributes = eType.GetProperties();
            var errorKey = string.Empty;
            builder.Append("INSERT INTO ");
            //|| (entity.MapperKeyIsNotIdentity())
            try
            {
                builder.Append(targetTable);
                foreach (var field in entity.ParametersOrColumns())
                {
                    if ((entity.Keys().Where(k => k.Key == field.Key).Count() < 1) )
                    {
                        var property = entityAttributes.Where(e => e.Name == field.Key).First();
                        errorKey = field.Value;
                        builderField.AppendFormat("{0}, \n", field.Value);
                        var propertyValue = AttributesBuilder(entity, property);
                        if (propertyValue != null)
                            builderFieldValue.AppendFormat("'{0}', \n", propertyValue);
                        else
                            builderFieldValue.AppendFormat("NULL,\n ", field.Value);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unrecognized key " + errorKey, e);
            }
            var fields = builderField.ToString();
            var values = builderFieldValue.ToString();

            builder.AppendFormat("({0}) \n VALUES ({1})",
                                    clearLastQuote(fields),
                                    clearLastQuote(values));

            buildedInsert = builder.ToString();
            builder = null;
            builderField = null;
            builderFieldValue = null;

            return buildedInsert;
        }

        /// <summary>
        /// Make a Delete to 
        /// </summary>
        /// <param name="entity">E</param>
        /// <returns>string</returns>
        public static string Delete(DataModel entity, string targetTable)
        {
            var buildedDelete = string.Empty;
            var builder = new StringBuilder();
            var builderField = new StringBuilder();
            var builderFieldValue = new StringBuilder();
            var eType = entity.GetType();
            var errorKey = string.Empty;
            builder.Append("DELETE FROM ");

            try
            {
                builder.Append(targetTable);
                builder.AppendFormat(" \n {0} \n ", Where(entity));
            }
            catch (Exception e)
            {
                throw new Exception("Unrecognized key " + errorKey, e);
            }

            buildedDelete = builder.ToString();
            builder = null;
            builderField = null;
            builderFieldValue = null;

            return buildedDelete;
        }

        /// <summary>
        /// Make a Update to 
        /// </summary>
        /// <param name="entity">E</param>
        /// <returns></returns>
        public static string Update(DataModel entity, string targetTable)
        {
            var buildedUpdate = string.Empty;
            var builder = new StringBuilder();
            var builderFieldValue = new StringBuilder();
            var trace = new StringBuilder();
            var eType = entity.GetType();
            try
            {
                var entityIdentifier = targetTable;
                builder.Append("UPDATE \n");
                builder.Append(entityIdentifier);
                builder.Append(" SET \n");
                var entityAttributes = entity.GetType().GetProperties();
                trace.Append("1- montando update parse " + entityIdentifier + "\n");

                foreach (var field in entity.ParametersOrColumns())
                {
                    if (entity.Keys().Where(k => k.Key == field.Key).Count() < 1)
                    {
                        var property = entityAttributes.Where(e => e.Name == field.Key).First();
                        trace.Append("entity property " + field.Value + "\n");
                        var propertyValue = AttributesBuilder(entity, property);
                        if (propertyValue != null)
                            builderFieldValue.AppendFormat("{0}='{1}',\n ", field.Value, propertyValue);
                        else
                            builderFieldValue.AppendFormat("{0}=NULL,\n ", field.Value);
                    }
                }
                var values = builderFieldValue.ToString();

                builder.AppendFormat("{0} \n {1} \n ", clearLastQuote(values), Where(entity));
                buildedUpdate = builder.ToString();
                builder = null;
                builderFieldValue = null;
            }
            catch (Exception dex)
            {
                throw new Exception("[DAS - Error] " + trace.ToString(), dex);
            }
            return buildedUpdate;
        }

        private static string clearLastQuote(string input)
        {
            return input.Substring(0, input.LastIndexOf(","));
        }

        /// <summary>
        /// To Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static string Where(DataModel entity)
        {
            var buildedWhere = new StringBuilder();
            buildedWhere.Append(" WHERE \n ");
            var and = string.Empty;
            foreach (var itemKey in entity.Keys())
            {
                var property = entity.GetType().GetProperty(itemKey.Key);
                var propertyValue = AttributesBuilder(entity, property);
                buildedWhere.AppendFormat("{0} {1}='{2}'", and, itemKey.Value, propertyValue);
                and = "AND";
            }
            return buildedWhere.ToString();
        }

        internal static object AttributesBuilder(DataModel o, PropertyInfo property)
        {
            var propertyValue = property.GetValue(o, null);
            switch (property.PropertyType.Name)
            {
                case "DateTime":
                    var dateValue = Convert.ToDateTime(propertyValue);
                    propertyValue = dateValue.ToString("yyyy-MM-dd hh:mm:ss");
                    break;
                case "Nullable`1":
                    if (propertyValue != null)
                    {
                        var _dateValue = Convert.ToDateTime(propertyValue);
                        propertyValue = _dateValue.ToString("yyyy-MM-dd hh:mm:ss");
                    }
                    else
                    {
                        propertyValue = null;
                    }
                    break;
            }
            return propertyValue;
        }

    }
}
