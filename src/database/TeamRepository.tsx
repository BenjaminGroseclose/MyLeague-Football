import { ITable, DATA_TYPE } from 'jsstore';
import { BaseRepository } from './BaseRepository';

export interface ITeam {
  id: number,
  name: string,
  city: string
};

export const TeamsTable : ITable = {
  name: "Teams",
  columns: {
    id: { primaryKey: true, autoIncrement: true, dataType: DATA_TYPE.Number },
    name: { notNull: true, dataType: DATA_TYPE.String },
    city: { notNull: true, dataType: DATA_TYPE.String }
  }
};

export class TeamRepository extends BaseRepository {
  tableName: string;

  constructor() {
    super();
    this.tableName = "Teams"
  }

  public get(): Promise<ITeam[]> {
    return this.Connection.select<ITeam>({
      from: this.tableName
    });
  };

  public getById(id: number): Promise<ITeam[]> {
    return this.Connection.select<ITeam>({
      from: this.tableName,
      where: { id: id },
      limit: 1
    });
  };

  public insert(team: ITeam): Promise<number | ITeam[]> {
    return this.Connection.insert<ITeam>({
      into: this.tableName,
      values: [team],
      return: true
    })
  };

  //public update(team: ITeam)

}