using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankTransactionApi.DataAccess;
using BankTransactionApi.Models;
using BankTransactionApi.Models.DbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace BankTransactionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        [HttpPost("NewExpense")]
        public NewExpenseResponseModel NewExpense([FromBody]NewExpenseRequestModel model)
        {
            if (model == null)
                return new NewExpenseResponseModel() { ErrorMessage = "Model is not valid", IsSuccess = false, ResultCode = "Error" };

            var expense = new Expense();
            expense.CompanyName = model.CompanyName;
            expense.CustomerNo = model.CustomerNo;
            expense.Price = model.Price;

            ExpenseManager.CreateExpense(expense);

            var customer = CustomerManager.GetCustomerByNo(model.CustomerNo);

            var informationModel = new InformationModel();
            informationModel.date = DateTime.Now;
            informationModel.email = customer.Email;
            informationModel.price = model.Price;

            AddQueue(informationModel);

            var response = new NewExpenseResponseModel();
            response.IsSuccess = true;
            response.ResultCode = "Success";

            return response;
        }

        [NonAction]
        private void AddQueue(InformationModel model)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "127.0.0.1";
            using (var connection=factory.CreateConnection())
            {
                using (var channel=connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "CreditCardInformationSms", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var message = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "CreditCardInformationSms", basicProperties: null, body: body);
                }
            }
        }
    }
}
