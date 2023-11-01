using Core.DTOs.General;
using Core.DTOs.SiteGeneric;
using Core.Services.Interfaces;
using Core.Utility;
using DataLayer.Entities.Blogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IGenericInsService _genericInsService;
        private readonly IBlogService _blogService;
        public IndexModel(IHttpContextAccessor httpContextAccessor, IGenericInsService genericInsService, IBlogService blogService)
        {
            _genericInsService = genericInsService;
            _httpContextAccessor = httpContextAccessor;
            _blogService = blogService;

        }
        public int? RCode { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public List<ProductSearchModel> ProductSearchModels { get; set; } = new();
        public IActionResult OnGet(int? code, string search)
        {
            string srch = search;
            RCode = code;
            //var role = await _genericInsService.GetLastActiveRoleAsync(User.Identity.Name);
            if (code != null)
            {
                CookieExtensions.SetHttpContextAccessor(_httpContextAccessor);
                CookieExtensions.SetCookie("refcode", code.Value.ToString(), DateTime.Now.AddHours(150));
            }
            if (!string.IsNullOrEmpty(search))
            {
                string[] words = search.Split('|');
                string searchType = string.Empty;
                if (words.Length > 1)
                {
                    
                    if (words[1].Trim() == "محصول")
                    {
                        searchType = "product";
                    }
                    if (words[1].Trim() == "مجله بیمه")
                    {
                        searchType = "blog";
                    }
                   
                }
            }

            
            //(bool IsSuccess, string Content) = _genericInsService.GetNextPayToken(15000, "100100", "09126617096", "https://melatins.com/", "بیمه زندگی", "320563", "IRT");
            //if (IsSuccess)
            //{
            //    string json = Content;
            //    dynamic data = JObject.Parse(json);
            //    string tid = data["trans_id"];
            //    string eUrl = "https://nextpay.org/nx/gateway/payment/" + tid;
            //    return Redirect(eUrl);
            //}
            return Page();
        }
        [BindProperty]
        public string Email { get; set; }


        public IActionResult OnPost()
        {
            return Page();
        }
        public async Task<IActionResult> OnGetSearchAct(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                ProductSearchModels = new()
            {
                new ProductSearchModel()
                {
                    Id = 1,
                    Title = "بیمه زندگی",
                    Comment = "با بیمه زندگی آرامش امروز و آسایش فردا را برای خود و خانواده به ارمغان بیاورید",
                    Link = "/Life-Insurance",
                    Type = "محصول بیمه ای",
                    EnType = "product",
                    Tags = new List<string>()
                    {
                        "life",
                        "عمر" ,
                        "متسمری",
                        "بازنشستگی",
                        "حادثه",
                        "درمان",
                        "از کارافتادگی",
                        "وام",
                        "سرمایه گذاری"
                    }
                },
                new ProductSearchModel()
                {
                    Id = 2,
                    Title = "بیمه ثالث",
                    Comment = "بیمه ثالث مسئولیت قانونی شماست، ما این مسئولیت را سریع و آنلاین پوشش می دهیم",
                    Link = "/Third-Party-Price-Inquiry",
                    Type = "محصول بیمه ای",
                    EnType = "product",
                    Tags= new List<string>()
                    {
                        "خودرو",
                        "ماشین",
                        "تصادف",
                        "خسارت",
                        "اتومبیل",
                        "تخفیف",
                        "عدم خسارت",
                        "third party"
                    }
                },
                new ProductSearchModel()
                {
                    Id = 3,
                    Title = "بیمه بدنه",
                    Comment = "بیمه ثالث خسارت خودرو مقصر را پوشش نمی دهد، شما به یک بیمه بدنه هم نیاز دارید",
                    Link = "/Car-Body-Price-Inquiry",
                    Type = "محصول بیمه ای",
                    EnType = "product",
                    Tags = new List<string>()
                    {
                        "بدنه اتومبیل",
                        "بدنه ماشین",
                        "بیمه بدنه اتومبیل",
                        "بیمه بدنه ماشین",
                        "خسارت بدنه خودرو",
                        "خسارت بدنه ماشین",
                        "خسارت بدنه اتومبیل",
                        "car",
                        "body"
                    }
                },
                new ProductSearchModel()
                {
                    Id = 4,
                    Title = "بیمه آتش سوزی",
                    Comment = "آتش سوزی، انفجار، زلزله، سیل و طوفان در یک بیمه اتش سوزی پوشش داده می شود",
                    Link = "/Fire-Insurance-Price-Inquiry",
                    Type = "محصول بیمه ای",
                    EnType = "product",
                    Tags = new List<string>()
                    {
                        "fire",
                        "آتش سوزی ساختمان",
                        "آتش سوزی مغازه",
                        "آتش سوزی خانه",
                        "آتش سوزی ویلا",
                    }
                },
                new ProductSearchModel()
                {
                    Id = 5,
                    Title = "بیمه مسئولیت",
                    Comment = "اگر شاغل هستید، کارفرما یا پزشک هستید یا مدیر یک ساختمان، بیمه مسئولیت نیاز دارید",
                    Link = "/Liabilty-Insurance",
                    Type = "محصول بیمه ای",
                    EnType = "product",
                    Tags = new List<string>()
                    {
                        "بیمه مسئولیت ساختمان",
                        "بیمه مسئولیت مجتمع"
                    }
                },
                new ProductSearchModel()
                {
                    Id = 6,
                    Title = "بیمه مسافرتی",
                    Comment = "عازم سفر هستید؟ سفارت کشور مقصد از شما بیمه میخواد؟ براتون آنلاین صدر می کنیم",
                    Link = "/Travel-Insurance",
                    Type = "محصول بیمه ای",
                    EnType = "product",
                    Tags = new List<string>()
                    {
                        "سفر",
                        "گردش",
                        "خارج",
                        "اروپا"
                    }
                }
            };
                ProductSearchModels = ProductSearchModels.Where(w => w.Title.Contains(search) || w.Comment.Contains(search) || w.Tags.Any(a => a.Contains(search))).ToList();
                List<Blog> blogs = await _blogService.GetBlogsAsync();
                blogs = blogs.Where(b => b.BlogTitle.Contains(search) || b.BlogSummary.Contains(search) || b.TagsList.Contains(search)).ToList();
                if (blogs.Count > 0)
                {
                    ProductSearchModels.AddRange(blogs.Select(b => new ProductSearchModel()
                    {
                        Title = b.BlogTitle,
                        Comment = b.BlogSummary,
                        Link = "/blog/d/" + b.BlogUrl,
                        Type = "مجله بیمه",
                        EnType = "blog",
                        Tags = b.TagsList.ToList()
                    }).ToList());
                }
                List<IGrouping<string, ProductSearchModel>> SRCH = ProductSearchModels.GroupBy(g => g.EnType).OrderBy(r => r.Key).ToList();
                return new JsonResult(SRCH.OrderBy(r => r.Key).ToList());
            }
            return NotFound("پارامتر مشخص نشده است !");
            
        }
        

    }
}
