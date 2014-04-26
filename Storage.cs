using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using SQLite;

namespace vkapp
{
    class Storage<T>
    {
        public Storage()
        {
            IfStorageExists = false;

            Create();
        }

        private async void Create()
        {
            await CreateStorageFile();
        }

        private async Task CreateStorageFile()
        {
            try
            {
                storageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("Storage.db", CreationCollisionOption.OpenIfExists);
                IfStorageExists = true;
            }
            catch
            {
                throw new VKAppException("Error creating storage file");
            }
        }

        private StorageFile storageFile;

        private static Boolean IfStorageExists;

        public async Task<Boolean> Delay()
        {
            if (!IfStorageExists)
                await CreateStorageFile();

            return IfStorageExists;
        }

        public void UpdateData(T item)
        {
            SQLiteConnection connection = null;

            try
            {
                connection = new SQLiteConnection(storageFile.Path);
                {
                    connection.CreateTable<T>();
                    connection.InsertOrReplace(item);
                }
            }
            catch (SQLite.SQLiteException e)
            {
                throw new SQLiteException(e.Message);
            }
            finally
            {
                try
                {
                    connection.Close();
                }
                catch (SQLiteException)
                {
                    
                }
            }
        }

        public AuthData GetActiveUser()
        {
            SQLiteConnection connection = null;

            try
            {
                connection = new SQLiteConnection(storageFile.Path);
                Log.Logger.Log.Error("sex");
                return connection.Query<AuthData>("select * from AuthData where IsActive = 1").SingleOrDefault();
            }
            catch (SQLite.SQLiteException)
            {
                Log.Logger.Log.Error("FUCK");
            }
            finally
            {
                try
                {
                    connection.Close();
                }
                catch (SQLiteException)
                {
                    
                }
            }

            return null;
        }
    }
}
