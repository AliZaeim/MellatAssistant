using DataLayer.Entities.Blogs;
using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IBlogService
    {
        #region Generics
        Task SaveAsync();
        void Save();
        Task<List<User>> GetUsersAsAuthorAsync();
        Task<Role> GetUserLastRoleAsync(int userId);
        Task<Role> GetUserLastActiveRoleAsync(int userId);
        Task<User> GetUserByIdAsync(int userId);
        Task<string> GetUserFullNameAsync(int userId);
        Task<DateTime?> GetUserLastPostDateAsync(int userId);
        Task<List<Blog>> GetAuthorBlogs(int AuthorId);
        #endregion Generics
        #region BlogGroup
        Task<List<BlogGroup>> GetblogGroups();
        Task<BlogGroup> GetBlogGroupWithId(int Id);
        public Task<BlogGroup> GetBlogGroupWithEnName(string EnName);
        Task Create_BlogGroup(BlogGroup blogGroup);
        void Edit_BlogGroup(BlogGroup blogGroup);
        bool Remove_BlogGroup(int Id);
        bool ExistBlogGroup(int Id);

        #endregion BlogGroup
        #region Blog
        public Task<List<Blog>> GetBlogsAsync();
        public Task<Blog> GetBlogByIdAsync(Guid id);
        public Task<Blog> GetBlogByIdAsync(string id);
        public Task<Blog> GetBlogByUrlAsync(string url);
        public void Create_Blog(Blog blog);
        public void Edit_Blog(Blog blog);
        public bool Remove_Blog(Guid Id);
        public Task<bool> ExistBlogUrlAsync(string Url);
        public Task<bool> ExistBlogUrlAsync(Guid Id, string Url);
        public Task<List<Blog>> GetBlogsWithDateAsync(string date);
        public Task<List<Blog>> GetBlogsWithGroupAsync(int groupId);
        public Task<List<Blog>> GetBlogsAsPodcast();
        public Task<List<Blog>> GetBlogsAsVideo();
        public Task<List<Blog>> GetBlogsWithSearchAsync(string exp);
        public Task<List<string>> GetBlogsUsefullTagsAsync(int count);
        public Task<List<Blog>> GetBlogsWithYearMounth(int year, int mounth);
        public Task<bool> ExistBlogsWithYearMounth(int year, int mounth);
        public int AddBlogViewCount(Guid id);
        
        public Task<List<Blog>> GetBlogsWithTagAsync(string tag);
        public bool ExistBlogbyId(Guid guid);
       
        
        #endregion BLog
        #region BlogComment
        public void CreateBlogComment(BlogComment blogComment);
        public Task<List<BlogComment>> GetBlogCommentsAsync();
        public void EditBlogComment(BlogComment blogComment);
        public Task<BlogComment> GetBlogCommentByIdAsync(int Id);
        #endregion

    }
}
