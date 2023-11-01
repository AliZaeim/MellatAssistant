using Core.DTOs.Admin;
using Core.Services.Interfaces;
using DataLayer.Entities.Supplementary;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.UsersPanel.Controllers
{
    [Area("UsersPanel")]
    [Authorize]
    public class ConversationsController : Controller
    {
        private readonly ICompService _compService;
        public ConversationsController(ICompService compService)
        {
            _compService = compService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _compService.GetConversationsByUserAsync(User.Identity.Name));
        }
        public async Task<IActionResult> Create(int? pId, int? repId)
        {
            string recinfo = string.Empty;
            if (repId != null)
            {
                Conversation repConversaion = await _compService.GetConversationByIdAsync((int)repId);
                if (repConversaion != null)
                {
                    recinfo = repConversaion.SenderNC + "-" + repConversaion.SenderFullName;
                }
            }
            ConversationVM conversationVM = new()
            {
                ParentId = pId,
                GetReply = true
            };
            if (pId == null)
            {
                if (string.IsNullOrEmpty(recinfo))
                {
                    if (User.Identity.Name == "0000000000")
                    {
                        List<User> users = await _compService.GetUsersAsync();
                        users = users.Where(w => w.IsActive && w.NC != User.Identity.Name).ToList();
                        conversationVM.Users = users;
                        if (users.Count == 0)
                        {
                            return Content("کاربری برای ارسال پیام موجود نمی باشد !");
                        }
                    }
                    else
                    {
                        conversationVM.UserInfos.Add("0000000000-سرپرستی فروش");
                    }
                }
                else
                {
                    conversationVM.UserInfos.Add(recinfo);
                }
            }
            else
            {
                Conversation conversation = await _compService.GetConversationByIdAsync((int)pId);
                if (string.IsNullOrEmpty(recinfo))
                {
                    recinfo = conversation.RecepiesInfo;
                    conversationVM.UserInfos.Add(recinfo);
                }
                else
                {
                    conversationVM.UserInfos.Add(recinfo);
                }
                conversationVM.Subject = conversation.Subject;
                conversationVM.Title = "<h4 class='text-xs-center alert alert-success'>" + "پاسخ به پیام " + "<br />" + conversation.SenderFullName + "<br />" + "با موضوع " + conversation.Subject + "</h4>";
            }
            if (repId != null)
            {
                Conversation rep = await _compService.GetConversationByIdAsync((int)repId);
                conversationVM.ParentId = repId;
                conversationVM.Title = "<h4 class='text-xs-center alert alert-success'>" + "پاسخ به پیام " + "<br/>" + rep.SenderFullName + "<br />" + "با موضوع " + rep.Subject + "</h4>";
            }
            return View(conversationVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConversationVM conversationVM)
        {
            if (!ModelState.IsValid)
            {
                if (User.Identity.Name == "0000000000")
                {
                    List<User> users = await _compService.GetUsersAsync();
                    users = users.Where(w => w.IsActive && w.NC != User.Identity.Name).ToList();
                    conversationVM.Users = users;
                }
                else
                {
                    conversationVM.Users.Add(await _compService.GetUserByName(User.Identity.Name));
                }
                return View(conversationVM);
            }
            User Sender = await _compService.GetUserByName(User.Identity.Name);
            Conversation conversation = new()
            {
                Subject = conversationVM.Subject,
                Message = conversationVM.Message,
                SenderNC = User.Identity.Name,
                SenderFullName = Sender.FullName,
                RegDate = DateTime.Now,
                ParentId = conversationVM.ParentId,
                IsActive = true,
            };
            string RecInfo = string.Empty;
            foreach (var item in conversationVM.UserInfos)
            {
                if (item != conversationVM.UserInfos.LastOrDefault())
                {
                    RecInfo += item + Environment.NewLine;
                }
                else
                {
                    RecInfo += item;
                }
            }
            conversation.GetReply = conversationVM.GetReply;
            conversation.RecepiesInfo = RecInfo;
            _compService.CreateConversation(conversation);
            await _compService.SaveChangesAsync();
            if (conversation.ParentId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("ShowConv", new { Id = (int)conversation.ParentId });
            }
        }
        public async Task<IActionResult> Details(int? parentId, int id)
        {
            Conversation conversation = null;
            if (parentId == null)
            {
                conversation = await _compService.GetConversationByIdAsync(id);
            }
            else
            {
                conversation = await _compService.GetConversationByIdAsync((int)parentId);
            }
            if (conversation == null)
            {
                return NotFound("پیامی موجود نمی باشد !");
            }
            bool edit = false;
            List<string> receps = conversation.RecepiesList.ToList();
            receps = receps.Where(w => !string.IsNullOrEmpty(w)).ToList();

            if (conversation.SenderNC != User.Identity.Name)
            {
                if (!string.IsNullOrEmpty(conversation.RecepiesInfo))
                {
                    if (receps.Any(a => a.Substring(0, a.IndexOf("-")) == User.Identity.Name))
                    {
                        if (!string.IsNullOrEmpty(conversation.Readers))
                        {
                            List<string> readers = conversation.ReadersList.ToList();
                            readers = readers.Where(w => !string.IsNullOrEmpty(w)).ToList();

                            if (!readers.Any(a => a.Substring(0, a.IndexOf("-")) == User.Identity.Name))
                            {
                                conversation.Readers += Environment.NewLine + User.Identity.Name + "-" + DateTime.Now;
                                edit = true;
                            }

                        }
                        else
                        {
                            conversation.Readers += User.Identity.Name + "-" + DateTime.Now;
                            edit = true;
                        }
                    }
                }

                if (edit == true)
                {
                    _compService.EditConversation(conversation);
                    await _compService.SaveChangesAsync();
                }
            }
            ViewData["id"] = id;

            return View(conversation);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _compService.GetConversationByIdAsync((int)id);
            if (conversation == null)
            {
                return NotFound();
            }
            if (conversation.SenderNC != User.Identity.Name)
            {
                return NotFound("پیام قابل ویرایش نیست !");
            }
            int? parentId = conversation.ParentId;
            List<string> RecepsCode = conversation.RecepiesList.ToList().Select(x => x.Split("-")[0]).ToList();
            ConversationVM conversationVM = new ConversationVM()
            {
                ParentId = conversation.ParentId,
                SelectedRecepsCode = RecepsCode

            };
            if (conversation.ParentId == null)
            {
                if (User.Identity.Name == "0000000000")
                {
                    List<User> users = await _compService.GetUsersAsync();
                    users = users.Where(w => w.IsActive && w.NC != User.Identity.Name).ToList();
                    conversationVM.Users = users;
                }
                else
                {
                    conversationVM.UserInfos.Add("0000000000-سرپرستی فروش");
                }
            }
            else
            {
                Conversation Pconversation = await _compService.GetConversationByIdAsync((int)conversation.ParentId);
                conversationVM.UserInfos.Add(Pconversation.RecepiesInfo);

            }
            conversationVM.Subject = conversation.Subject;
            conversationVM.Message = conversation.Message;
            conversationVM.GetReply = conversation.GetReply;
            return View(conversationVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ConversationVM conversationVM)
        {
            var conversation = await _compService.GetConversationByIdAsync(conversationVM.Id);
            if (!ModelState.IsValid)
            {
                string recinfo = string.Empty;

                int? parentId = conversation.ParentId;
                List<string> RecepsCode = conversation.RecepiesList.ToList().Select(x => x.Split("-")[0]).ToList();
                conversationVM.SelectedRecepsCode = RecepsCode;
                if (conversation.ParentId == null)
                {
                    if (User.Identity.Name == "0000000000")
                    {
                        List<User> users = await _compService.GetUsersAsync();
                        users = users.Where(w => w.IsActive && w.NC != User.Identity.Name).ToList();
                        conversationVM.Users = users;
                    }
                    else
                    {
                        conversationVM.UserInfos.Add("0000000000-سرپرستی فروش");
                    }
                }
                else
                {

                    conversationVM.UserInfos.Add(conversation.SenderNC + "-" + conversation.SenderFullName);
                    conversationVM.Subject = conversation.Subject;
                }
                return View(conversationVM);
            }
            Conversation EConversation = await _compService.GetConversationByIdAsync(conversationVM.Id);

            EConversation.Subject = conversationVM.Subject;
            EConversation.Message = conversationVM.Message;
            EConversation.GetReply = conversationVM.GetReply;
            string RecInfo = string.Empty;
            foreach (var item in conversationVM.UserInfos)
            {
                if (item != conversationVM.UserInfos.LastOrDefault())
                {
                    RecInfo += item + Environment.NewLine;
                }
                else
                {
                    RecInfo += item;
                }
            }
            EConversation.RecepiesInfo = RecInfo;
            _compService.EditConversation(EConversation);
            await _compService.SaveChangesAsync();
            if (conversation.ParentId == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("ShowConv", new { Id = (int)EConversation.ParentId });
            }



        }
        public async Task<IActionResult> GetConversation(int Id)
        {
            Conversation conversation = await _compService.GetConversationByIdAsync(Id);
            return PartialView(conversation);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Conversation conversation = await _compService.GetConversationByIdAsync((int)id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conversation = await _compService.GetConversationByIdAsync(id);
            var Topconversation = await _compService.GetTopParent_ofConversationAsync(id);
            conversation.IsDeleted = true;
            _compService.EditConversation(conversation);
            await _compService.SaveChangesAsync();
            if (conversation.ParentId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(ShowConv), Topconversation.Id);
        }
        [HttpPost]
        public async Task<bool> RemoveConv(int id)
        {
            Conversation conversation = await _compService.GetConversationByIdAsync(id);
            if (conversation == null)
            {
                return false;
            }
            conversation.IsDeleted = true;
            _compService.EditConversation(conversation);
            await _compService.SaveChangesAsync();
            return true;
        }
        public async Task<IActionResult> ShowConv(int Id)
        {
            Conversation conversation = await _compService.GetConversationByIdAsync(Id);

            if (conversation == null)
            {
                return NotFound();
            }
            List<string> readers = null;
            if (!string.IsNullOrEmpty(conversation.Readers))
            {
                readers = conversation.ReadersList.ToList();
            }
            List<string> receps = null;
            if (!string.IsNullOrEmpty(conversation.RecepiesInfo))
            {
                receps = conversation.RecepiesList.ToList();
            }
            if (receps.Any(a => a.Substring(0, a.IndexOf("-")) == User.Identity.Name))
            {
                if (!string.IsNullOrEmpty(conversation.Readers))
                {
                    if (!readers.Any(a => a.Substring(0, a.IndexOf("-")) == User.Identity.Name))
                    {
                        await AddReaderToConv(Id);
                    }
                }
                else
                {
                    await AddReaderToConv(Id);
                }

            }

            if (conversation.ParentId != null)
            {
                conversation = await _compService.GetTopParent_ofConversationAsync(Id);
            }
            if (User.Identity.Name != "0000000000")
            {
                bool b1 = receps.Any(a => a.Substring(0, a.IndexOf("-")) == User.Identity.Name);
                
                if (b1 == false && conversation.SenderNC != User.Identity.Name)
                {
                    return Content("مجاز به مشاهده اطلاعات پیام نمی باشید !");
                }
            }

            return View(conversation);

        }
        public async Task<bool> AddReaderToConv(int id)
        {
            Conversation conversation = await _compService.GetConversationByIdAsync(id);
            if (conversation == null)
            {
                return false;
            }
            bool edit = false;
            if (conversation.SenderNC != User.Identity.Name)
            {
                if (!string.IsNullOrEmpty(conversation.Readers))
                {
                    List<string> readers = conversation.ReadersList.ToList();
                    readers = readers.Where(w => !string.IsNullOrEmpty(w)).ToList();
                    if (!readers.Any(a => a.Substring(0, a.IndexOf("-")) == User.Identity.Name))
                    {
                        conversation.Readers += Environment.NewLine + User.Identity.Name + "-" + DateTime.Now;
                        edit = true;
                    }
                }
                else
                {
                    conversation.Readers += User.Identity.Name + "-" + DateTime.Now;
                    edit = true;
                }
            }
            if (edit)
            {
                _compService.EditConversation(conversation);
                await _compService.SaveChangesAsync();
                return true;
            }
            return false;


        }
        public async Task<IActionResult> ShowReaders(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var conversation = await _compService.GetConversationByIdAsync((int)id);
            if (conversation == null)
            {
                return NotFound();
            }
            return View(conversation);
        }
        public async Task<IActionResult> ShowReceps(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var conversation = await _compService.GetConversationByIdAsync((int)id);
            if (conversation == null)
            {
                return NotFound();
            }
            return View(conversation);
        }
        public async Task<int> GetUnreadConvCount()
        {
            List<Conversation> UnreadInnerMessages = await _compService.GetUnreadConversationsByNameAsync(User.Identity.Name);
            if (UnreadInnerMessages == null)
            {
                return 0;
            }
            return UnreadInnerMessages.Count();
        }

    }
}
