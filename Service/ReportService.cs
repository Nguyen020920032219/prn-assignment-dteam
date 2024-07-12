using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    
    public class ReportService
    {
        ReportRepository _repository;
        public ReportService() { 
            _repository = new ReportRepository();
        }

        public List<Order> GetListOrders(DateOnly fromDate, DateOnly toDate)
        {
            List<Order> orders = _repository.GetAll();
            List<Order> result = new List<Order>();
            foreach (Order order in orders)
            {
                if(order.Date >= fromDate && order.Date <= toDate && order.Status==true)
                {
                    result.Add(order);
                }
            }
            return result;
        }
        public (List<Order>, decimal) GetOrders(DateOnly fromDate, DateOnly toDate)
        {
            List<Order> orders = GetListOrders(fromDate, toDate);
            decimal total = orders.Sum(order => (decimal)order.TotalPrice);
            return (orders, total);
        }
    }
}
