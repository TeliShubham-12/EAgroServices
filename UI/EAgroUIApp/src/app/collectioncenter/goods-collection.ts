export class GoodsCollection {
    constructor(
        public farmerId: number,
        public cropId: number,
        public containerType: string,
        public quantity: number,
        public weight: number,
    ){}
}
