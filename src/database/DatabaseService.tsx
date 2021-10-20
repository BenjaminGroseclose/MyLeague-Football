import { Connection, IDataBase } from 'jsstore'
import workerInjector from "jsstore/dist/worker_injector";
import { TeamsTable } from './TeamRepository';

export const connection = new Connection();
connection.addPlugin(workerInjector);

export const databaseName = "MyLeagueFootball";

function CreateDatabase() : IDataBase {
  var database: IDataBase = {
    name: databaseName,
    tables: [
      TeamsTable
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