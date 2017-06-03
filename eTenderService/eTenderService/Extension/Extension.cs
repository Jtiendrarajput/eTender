using eTenderService.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eTender.EncryptDecrypt;

namespace eTenderService.Extension
{
    public static class  Extension
    {
     
        public static T  DecryptEntity<T>(this T entity) where T: class
        {
            var encryptedProperties = entity.GetType().GetProperties()
            .Where(p => p.GetCustomAttributes(typeof(Encrypted), true).Any(a => p.PropertyType == typeof(String)));

            foreach (var property in encryptedProperties)
            {
                string encryptedValue = property.GetValue(entity, null) as string;
                if (!String.IsNullOrEmpty(encryptedValue))
                {
                    string value = encryptedValue.Decrypt();
                    //_db.Entry(entity).Property(property.Name).OriginalValue = value;
                    //_db.Entry(entity).Property(property.Name).IsModified = false;
                    property.SetValue(entity, value, null);
                }
            }
            return entity; 
        }

    }
}
