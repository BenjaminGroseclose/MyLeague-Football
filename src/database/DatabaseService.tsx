import { DATA_TYPE, Connection, IDataBase } from 'jsstore'

export const connection = new Connection(new Worker('jsstore.worker.js'));
export const databaseName = "MyLeagueFootball";

function CreateDatabase() : IDataBase {
  var database: IDataBase = {
    name: databaseName,
    tables: [
      
    ]
  };

  return database;
}

export const InitJsStore =  async () => {
  try {
    const isCreated = await connection.initDb(CreateDatabase());

    if (isCreated === true) {
      console.log("Successfully created database")
    } else {
      throw new Error("Failed to create database");
    }
  } catch (err) {
    console.error(err);
  }
}