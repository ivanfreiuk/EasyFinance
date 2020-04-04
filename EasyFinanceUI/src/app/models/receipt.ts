import { Category } from './category';
import { PaymentMethod } from './payment-method';
import { Currency } from './currency';
import { ReceiptPhoto } from './receipt-photo';

export class Receipt {
    public id: number;
    public merchant: string;
    public receiptPhotoId: number;
    public receiptPhoto: ReceiptPhoto;
    public categoryId: number;
    public category: Category;
    public paymentMethodId: number;
    public paymentMethod: PaymentMethod;
    public totalAmount: number;
    public currencyId: number;
    public currency: Currency;
    public purchaseDate: Date;
    public description: string;


    constructor() {
        this.id = 0;
        this.merchant = '';
        this.receiptPhotoId = null;
        this.categoryId = null;
        this.paymentMethodId = null;
        this.totalAmount = 0.00;
        this.currencyId = null;
        this.purchaseDate = new Date();
        this.description = ''
    }
}
