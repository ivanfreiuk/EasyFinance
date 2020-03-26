using EasyFinance.BusinessLogic.Models;
using EasyFinance.DataAccess.Entities;

namespace EasyFinance.BusinessLogic.Builders.Interfaces
{
    public interface IReceiptObjectDirector
    {
        Receipt ConstructReceipt(ScanText scanText);
    }
}
