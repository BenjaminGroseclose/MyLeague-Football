import { connection } from "./DatabaseService";

export class BaseRepository {

    get Connection() {
        return connection;
    }

}