export class ReceiptFilterCriteria {
    public userId: number;
    public dateFrom: Date;
    public dateTo: Date;
    public categoryId: number;
    public totalFrom: number;
    public totalTo: number;

    constructor() {
        this.userId = null;
        this.dateFrom = null;
        this.dateTo = null;
        this.categoryId = null;
        this.totalFrom = null;
        this.totalTo = null;
    }
}