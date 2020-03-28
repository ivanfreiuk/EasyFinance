import { Category } from './category';
import { PaymentMethod } from './payment-method';
import { Currency } from './currency';

export class Receipt {
    public id: string;
    public categoryId: number;
    public category: Category;
    public paymentMethodId: number;
    public paymentMethod:PaymentMethod;
    public totalAmount: number;
    public currencyId:number;
    public currency: Currency; 
    public purchaseDate: Date;
}

