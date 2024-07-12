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
    }
}
