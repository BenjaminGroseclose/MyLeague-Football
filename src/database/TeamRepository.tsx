import { ITable, DATA_TYPE } from 'jsstore';
import { BaseRepository } from './BaseRepository';

export const TeamsTable : ITable = {
  name: "Teams",
  columns: {
    id: {
      primaryKey: true,
      autoIncrement: true,
      dataType: DATA_TYPE.Number
    },
    name: {
      notNull: true,
      dataType: DATA_TYPE.String
    },
    city: {
      notNull: true,
      dataType: DATA_TYPE.String
    }
  }
}

export class TeamRepository extends BaseRepository {
  tableName: string;

  constructor() {
    super();
    this.tableName = "Teams"
  }

}