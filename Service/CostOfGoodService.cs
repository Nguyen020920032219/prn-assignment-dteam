using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CostOfGoodService
    {
        CostOfGoodRepository _costOfGoodRepo;

        public CostOfGoodService()
        {
            _costOfGoodRepo = new CostOfGoodRepository();
        }

        public void AddCostOfGood(CostOfGood costOfGood)
        {
            _costOfGoodRepo.Add(costOfGood);
        }

        public List<CostOfGood> GetCostOfGood()
        {
            return _costOfGoodRepo.GetAll();
        }

        public List<CostOfGood> GetListCostOfGood(DateOnly fromDate, DateOnly toDate)
        {
            List<CostOfGood> costOfGoods = _costOfGoodRepo.GetAll();
            List<CostOfGood> result = new List<CostOfGood>();
            foreach (CostOfGood costOfGood in costOfGoods)
            {
                if(costOfGood.Date >= fromDate && costOfGood.Date <= toDate)
                {
                    result.Add(costOfGood);
                }
            }
            return result;
        }

        public (List<CostOfGood>, decimal) GetOrdersCostOfGood(DateOnly fromDate, DateOnly toDate)
        {
            List<CostOfGood> costOfGoods = GetListCostOfGood(fromDate, toDate);
            decimal total = (decimal)costOfGoods.Sum(cost => cost.Quantity * cost.Price);
            return (costOfGoods, total);
        }

    }
}
