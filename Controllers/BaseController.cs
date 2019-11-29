using System;
using System.IO;
using System.Linq.Expressions;
using ClientNotifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepositoryDemo.GenericRepository;
using RepositoryDemo.Models;
using static ClientNotifications.Helpers.NotificationHelper;

namespace RepositoryDemo.Controllers
{
    public class BaseController<T> : Controller where T: class 
    {
        private readonly IClientNotification _clientNotification;

        public BaseController(IClientNotification clientNotification)
        {
            _clientNotification = clientNotification;
        }

        public void Notify(string message, NotificationType type = NotificationType.success){
            _clientNotification.AddToastNotification(message,type,null);
        }

        public void Create(IGenericRepository<T> repository, T t, string studentname){
            try{
                var tt = new Student();
                repository.Create(t);
                Notify($"{studentname} has been created!");
            }catch(Exception){
                Notify($"Could not create {studentname}. Please try again", NotificationType.error);
            }
        }

        public void Update(IGenericRepository<T> repository, T t, string modelName){
            try{
                
                repository.Update(t);
                Notify($"{modelName} has been updated");
            }catch(Exception){
                Notify($"Could not update {modelName}", NotificationType.error);
            }
        }


        public void Remove(IGenericRepository<T> repository, Expression<Func<T,bool>> predicate){
            try{
                var modelToRemove = repository.GetSingle(predicate);
                repository.Remove(modelToRemove);
                Notify("Deleted Successfully");
            }catch(Exception){
                Notify("Could not delete item.");
            }
        }

        public string GetUserId(UserManager<IdentityUser> userManager){
            return userManager.GetUserId(HttpContext.User);
        }

        public string UploadFile(IFormFile file){
            var fileName = Guid.NewGuid().ToString()+file.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/images",fileName);

            using(var fileStream = new FileStream(path,FileMode.Create)){
                file.CopyTo(fileStream);
            }

            return fileName;
        }
    }
}