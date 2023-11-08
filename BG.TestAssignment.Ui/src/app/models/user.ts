export class User {
    constructor(
        public id: number,
        public userName: string,
        public firstName: string,
        public lastName: string,
        public birthDate: Date,
        public address: string,
    ) {}
}
