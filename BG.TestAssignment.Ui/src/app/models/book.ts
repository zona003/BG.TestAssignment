import { Author } from "./author";

export class Book{
    constructor(
        public id: number,
        public title: string,
        public publishedDate: Date,
        public bookGenre: string,
        public authorsInBooks: Author[] | undefined
    ){}
}