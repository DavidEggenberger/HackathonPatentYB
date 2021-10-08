using Domain;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SharedContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Hubs
{
    public class DiagramHub : Hub
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext applicationDbContext;
        public DiagramHub(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            this.userManager = userManager;
            this.applicationDbContext = applicationDbContext;
        }
        //public async Task NewOnlineUser()
        //{
        //    ApplicationUser appUser = await userManager.GetUserAsync(Context.User);
        //    if (appUser.IsOnline is false)
        //    {
        //        appUser.IsOnline = true;
        //        appUser.TabsOpen = 1;
        //        await applicationDbContext.SaveChangesAsync();
        //        await Clients.All.SendAsync("NewUser", new ApplicationUserDTO
        //        {
        //            Id = appUser.Id,
        //            PictureUri = appUser.PictureUri,
        //            UserName = appUser.UserName
        //        });
        //        return;
        //    }
        //    if (appUser.IsOnline)
        //    {
        //        appUser.TabsOpen++;
        //        await applicationDbContext.SaveChangesAsync();
        //    }
        //}
        //public override async Task OnDisconnectedAsync(Exception ex)
        //{
        //    ApplicationUser appUser = await userManager.GetUserAsync(Context.User);
        //    if (appUser.TabsOpen > 0)
        //    {
        //        appUser.TabsOpen--;
        //        await applicationDbContext.SaveChangesAsync();
        //    }
        //    if (appUser.TabsOpen == 0)
        //    {
        //        appUser.IsOnline = false;
        //        await applicationDbContext.SaveChangesAsync();
        //        await Clients.All.SendAsync("RemoveUser", new ApplicationUserDTO
        //        {
        //            Id = appUser.Id,
        //            PictureUri = appUser.PictureUri,
        //            UserName = appUser.UserName
        //        });
        //    }
        //}
    }
}
