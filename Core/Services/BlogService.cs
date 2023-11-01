using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Blogs;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly MyContext _context;
        public BlogService(MyContext context)
        {
            _context = context;
        }
        #region Generics
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<User>> GetUsersAsAuthorAsync()
        {
            return await _context.Users.Include(r => r.UserRoles)
                .Where(w => w.IsActive && !string.IsNullOrEmpty(w.LevelofStudy) && !string.IsNullOrEmpty(w.FieldofStudy) && !string.IsNullOrEmpty(w.InsWokHistory)).ToListAsync();
        }
        public async Task<Role> GetUserLastRoleAsync(int userId)
        {
            List<UserRole> userRoles = await _context.UserRoles.Include(x => x.User).Include(x => x.Role)
                            .Where(w => w.User.Id == userId).ToListAsync();
            if (userRoles != null)
            {
                return userRoles.OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
            }
            return null;
        }
        public async Task<Role> GetUserLastActiveRoleAsync(int userId)
        {
            List<UserRole> userRoles = await _context.UserRoles.Include(x => x.User).Include(x => x.Role)
                            .Where(w => w.User.Id == userId).ToListAsync();
            if (userRoles != null)
            {
                return userRoles.Where(w => w.IsActive).OrderByDescending(x => x.RegisterDate).FirstOrDefault().Role;
            }
            return null;
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.Include(x => x.UserRoles).SingleOrDefaultAsync(x => x.Id == userId);
        }
        public async Task<string> GetUserFullNameAsync(int userId)
        {
            User user = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return string.Empty;
            }
            return user.FullName;
        }
        public async Task<DateTime?> GetUserLastPostDateAsync(int userId)
        {
            User user = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }
            List<Blog> blogs = await _context.Blogs.Where(w => w.BlogIsActive && w.AuthorId == userId).ToListAsync();
            if (blogs == null)
            {
                return null;
            }
            if (blogs.Count == 0)
            {
                return null;
            }
            return blogs.OrderByDescending(x => x.BlogDate).FirstOrDefault().BlogDate;
        }
        public async Task<List<Blog>> GetAuthorBlogs(int AuthorId)
        {
            return await _context.Blogs.Where(w => w.BlogIsActive && w.AuthorId == AuthorId).ToListAsync();
        }
        #endregion Generics

        #region Blog
        public void Create_Blog(Blog blog)
        {
            _context.Blogs.Add(blog);
        }
        public void Edit_Blog(Blog blog)
        {
            _context.Blogs.Update(blog);
        }
        public async Task<Blog> GetBlogByIdAsync(Guid id)
        {
            return await _context.Blogs.Include(r => r.BlogGroup).Include(r => r.BlogComments).SingleOrDefaultAsync(s => s.BlogId == id);
        }
        public async Task<Blog> GetBlogByUrlAsync(string url)
        {
            return await _context.Blogs.Include(x => x.BlogComments).Include(x => x.BlogGroup).SingleOrDefaultAsync(x => x.BlogUrl == url.Trim());
        }
        public async Task<List<Blog>> GetBlogsAsync()
        {
            return await _context.Blogs.Include(r => r.BlogGroup).Include(r => r.BlogComments).ToListAsync();
        }
        public bool Remove_Blog(Guid Id)
        {
            Blog blog = _context.Blogs.Find(Id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                return true;
            }
            return false;
        }
        public async Task<Blog> GetBlogByIdAsync(string id)
        {
            return await _context.Blogs.Include(r => r.BlogComments).Include(r => r.BlogGroup).SingleOrDefaultAsync(x => x.BlogId.ToString() == id);
        }
        public async Task<bool> ExistBlogUrlAsync(string Url)
        {
            return await _context.Blogs.AnyAsync(x => x.BlogUrl == Url.Trim());
        }
        public async Task<bool> ExistBlogUrlAsync(Guid Id, string Url)
        {
            return await _context.Blogs.AnyAsync(x => x.BlogId != Id && x.BlogUrl == Url);
        }
        public async Task<List<Blog>> GetBlogsWithDateAsync(string date)
        {
            List<Blog> archiveBlog = null;
            if (!string.IsNullOrEmpty(date))
            {
                string[] dte = date.Split('-');
                int pyear = 0; int pmounth = 0; int pday = 0;
                PersianCalendar pc = new();
                if (dte.Length != 0)
                {
                    if (dte.Length > 0)
                    {
                        pyear = int.Parse(dte[0].ToString());
                    }

                    if (dte.Length > 1)
                    {
                        pmounth = int.Parse(dte[1].ToString());
                    }
                    if (dte.Length > 2)
                    {
                        pday = int.Parse(dte[2].ToString());
                    }
                }
                if (pyear != 0)
                {
                    archiveBlog = await _context.Blogs.Where(w => pc.GetYear((DateTime)w.BlogDate) == pyear).ToListAsync();
                }
                if (pyear != 0 && pmounth != 0)
                {
                    archiveBlog = await _context.Blogs.Where(w => pc.GetYear((DateTime)w.BlogDate) == pyear && pc.GetMonth((DateTime)w.BlogDate) == pmounth).ToListAsync();
                }
                if (pyear != 0 && pmounth != 0 && pday != 0)
                {
                    archiveBlog = await _context.Blogs.Include(r => r.BlogGroup).Where(w => pc.GetYear((DateTime)w.BlogDate) == pyear && pc.GetMonth((DateTime)w.BlogDate) == pmounth && pc.GetDayOfMonth((DateTime)w.BlogDate) == pday).ToListAsync();
                }

            }
            return archiveBlog;
        }
        public async Task<List<Blog>> GetBlogsWithGroupAsync(int groupId)
        {

            return await _context.Blogs.Include(r => r.BlogGroup).Include(r => r.BlogComments).Where(w => w.BlogGroupId == groupId).ToListAsync();
        }
        
        public async Task<List<string>> GetBlogsUsefullTagsAsync(int count)
        {
            List<string> tags;
            if (count != 0)
            {
                tags =await _context.Blogs.OrderByDescending(r => r.BlogViewsCount).Select(s => s.BlogTags).Take(count).ToListAsync();
            }
            else
            {
                tags =await _context.Blogs.Select(w => w.BlogTags).OrderByDescending(r => r.Max()).Take(5).ToListAsync();
            }
            if (tags != null)
            {
                List<string> Result = new();
                foreach (var item in tags)
                {
                    string[] ttag = item.Split("-");
                    foreach (var tg in ttag.ToList())
                    {
                        if (Result.Any(a => a.Trim() == tg.Trim()) == false)
                        {
                            Result.Add(tg);
                        }
                    }
                }
                return Result.Distinct().ToList();
            }
            else
            {
                return null;
            }



        }
        public int AddBlogViewCount(Guid id)
        {
            Blog blog = _context.Blogs.SingleOrDefault(s => s.BlogId == id);
            if (blog != null)
            {
                int vCount = (int)blog.BlogViewsCount;
                vCount += 1;
                blog.BlogViewsCount = vCount;
                _context.Blogs.Update(blog);
                _context.SaveChanges();
                return vCount;
            }
            return 0;
        }
        public async Task<List<Blog>> GetBlogsWithSearchAsync(string exp)
        {
            if (!string.IsNullOrEmpty(exp))
            {
                exp = "%" + exp + "%";
                return await _context.Blogs.Include(r => r.BlogGroup).Include(r => r.BlogComments).Where(w => w.BlogIsActive ).Where(w => EF.Functions.Like(w.BlogText, exp)
                || EF.Functions.Like(w.BlogTitle, exp) || EF.Functions.Like(w.BlogSummary, exp)
                || EF.Functions.Like(w.BlogTags, exp)).ToListAsync();
            }
            return null;
        }
        public async Task<BlogGroup> GetBlogGroupWithEnName(string EnName)
        {
            return await _context.BlogGroups.Include(r => r.Blogs).SingleOrDefaultAsync(x => x.BlogGroupEnTitle.Trim().Replace(" ","-") == EnName);
        }
        public async Task<List<Blog>> GetBlogsWithYearMounth(int year, int mounth)
        {
            PersianCalendar pc = new();
            List<Blog> blogs = await _context.Blogs.Where(w => w.BlogIsActive).ToListAsync();
            blogs = blogs.Where(w => pc.GetYear((DateTime)w.BlogDate) == year).ToList();
            blogs = blogs.Where(w => pc.GetMonth((DateTime)w.BlogDate) == mounth).ToList();
            return blogs.ToList();
        }
        public async Task<bool> ExistBlogsWithYearMounth(int year, int mounth)
        {
            PersianCalendar pc = new();
            List<Blog> blogs = await _context.Blogs.Where(w => w.BlogIsActive).ToListAsync();
            return blogs.Any(a => pc.GetYear((DateTime)a.BlogDate) == year && pc.GetMonth((DateTime)a.BlogDate) == mounth);
            
        }
        public async Task<List<Blog>> GetBlogsWithTagAsync(string tag)
        {
            List<Blog> blogs = await _context.Blogs.Include(x => x.BlogComments).Include(x => x.BlogGroup).ToListAsync();
            blogs = blogs.Where(w => w.TagsList.Any(x => x == tag)).ToList();
            return blogs.ToList();
        }
        public async Task<List<Blog>> GetBlogsAsPodcast()
        {
            return await _context.Blogs.Where(w => w.HasVoice).ToListAsync();
        }

        public async Task<List<Blog>> GetBlogsAsVideo()
        {
            return await _context.Blogs.Where(w => w.HasVideo).ToListAsync();
        }

        #endregion Blog
        #region BlogGroup
        public async Task Create_BlogGroup(BlogGroup blogGroup)
        {
            await _context.BlogGroups.AddAsync(blogGroup);
        }
        public void Edit_BlogGroup(BlogGroup blogGroup)
        {

            _context.BlogGroups.Update(blogGroup);
        }
        public async Task<List<BlogGroup>> GetblogGroups()
        {
            return await _context.BlogGroups.Include(r => r.Blogs).ToListAsync();
        }
        public async Task<BlogGroup> GetBlogGroupWithId(int Id)
        {
            return await _context.BlogGroups.FindAsync(Id);
        }
        public bool Remove_BlogGroup(int Id)
        {
            BlogGroup blogGroup = _context.BlogGroups.Include(r => r.Blogs).SingleOrDefault(s => s.BlogGroupId == Id);
            if (blogGroup != null)
            {

                if (blogGroup.Blogs.Count == 0)
                {
                    _context.BlogGroups.Remove(blogGroup);
                    return true;
                }

            }
            return false;
        }

        public bool ExistBlogGroup(int Id)
        {
            return _context.BlogGroups.Any(a => a.BlogGroupId == Id);
        }

        public bool ExistBlogbyId(Guid guid)
        {
            return _context.Blogs.Any(a => a.BlogId == guid);
        }
        #endregion BlogGroup
        #region BlogComment
        public void CreateBlogComment(BlogComment blogComment)
        {
            _context.BlogComments.Add(blogComment);
        }
        public async Task<List<BlogComment>> GetBlogCommentsAsync()
        {
            return await _context.BlogComments.Include(x => x.Blog).ToListAsync();
        }
        public void EditBlogComment(BlogComment blogComment)
        {
            _context.BlogComments.Update(blogComment);
        }
        public async Task<BlogComment> GetBlogCommentByIdAsync(int Id)
        {
            return await _context.BlogComments.Include(x => x.Blog).SingleOrDefaultAsync(x => x.Id == Id);
        }      

        #endregion


    }
}
