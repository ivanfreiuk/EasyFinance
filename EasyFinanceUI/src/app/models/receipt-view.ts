export class ReceiptView {
    public id: number;
    public imageURL: any;
    public merchant: string;
    public categoryName: string;
    public paymentMethodName: string;
    public totalAmount: number;
    public currencyName: string;
    public purchaseDate: Date;
    public description: string;


    constructor() {
        this.id = 0;
        this.imageURL = '';
        this.merchant = '';
        this.categoryName = '';
        this.paymentMethodName = '';
        this.totalAmount = 0.00;
        this.currencyName = '';
        this.purchaseDate = new Date();
        this.description = ''
    }
}