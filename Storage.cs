using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using vkapp.Log;
using SQLite;

namespace vkapp
{
    class Storage<T>
    {
        public Storage()
        {
    
        }

        private static StorageFile storageFile;

        public async Task CreateStorageFile()
        {
            if (storageFile == null)
            {
                try
                {
                    storageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync("Storage.db", CreationCollisionOption.OpenIfExists);
                }
                catch (System.IO.IOException e)
                {
                    Logger.Log.Error("Error creating storagefile : " + e.Message);
                }
            }
        }

        public void UpdateData(T item)
        {
            SQLiteConnection connection = null;

            try
            {
                connection = new SQLiteConnection(storageFile.Path);
                connection.CreateTable<T>();
                connection.InsertOrReplace(item);
            }
            catch (SQLite.SQLiteException e)
            {
                Logger.Log.Error("Error while making Updating Data : " + e.Message);
            }
            finally
            {
                try
                {
                    connection.Close();
                }
                catch (SQLiteException e)
                {
                    Logger.Log.Error("Could not close SQLiteconnection : " + e.Message);
                }
            }
        }

        public AuthData GetActiveUser()
        {
            SQLiteConnection connection = null;

            try
            {
                connection = new SQLiteConnection(storageFile.Path);
                connection.CreateTable<AuthData>();

                return connection.Query<AuthData>("select * from AuthData where IsActive = 1").SingleOrDefault();
            }
            catch (SQLite.SQLiteException e)
            {
                Logger.Log.Error("Cant get Active user from Data.db : " + e.Message);
            }
            finally
            {
                try
                {
                    connection.Close();
                }
                catch (SQLite.SQLiteException e)
                {
                    Logger.Log.Error("Could not close SQLiteconnection : " + e.Message);
                }
            }

            return null;
        }

        public void Delete(Object key)
        {
            try
            {
                SQLiteConnection connection = null;

                try
                {
                    connection = new SQLiteConnection(storageFile.Path);
                    connection.CreateTable<AuthData>();
                    connection.Delete<AuthData>(key);
                }
                catch (SQLite.SQLiteException e)
                {
                    Logger.Log.Error("Cant get Active user from Data.db : " + e.Message);
                }
                finally
                {
                    try
                    {
                        connection.Close();
                    }
                    catch (SQLite.SQLiteException e)
                    {
                        Logger.Log.Error("Could not close SQLiteconnection : " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error(e.Message);
            }
        }
    }
}
