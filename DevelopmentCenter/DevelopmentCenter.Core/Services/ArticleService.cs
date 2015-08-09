using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevelopmentCenter.Core.Models;
using DevelopmentCenter.Core.Repositories;
using DevelopmentCenter.Infrastructure;


namespace DevelopmentCenter.Core.Services
{
    public interface IArticleService : IDependency
    {
        IEnumerable<Article> List();
        void Add(Article article);
        void Update(Article article);
        void Delete(int id);
        Article Get(int id);

        IEnumerable<Article> GetByChannelTag(string tagName, int pageIndex, int pageSize, ref int totalCount);
        IEnumerable<Article> GetByColumnTag(string tagName, int pageIndex, int pageSize, ref int totalCount);
    }

    public class ArticleService : IArticleService
    {
        private readonly AppDbContext _appDbContext;
        public ArticleService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Article> List()
        {
            return _appDbContext.Articles.OrderByDescending(x => x.CreatedUtc).ToList();
        }

        public void Add(Article article)
        {
            _appDbContext.Articles.Add(article);
            _appDbContext.SaveChanges();
        }

        public void Update(Article article)
        {
            //_appDbContext.Articles.Attach(article);
            _appDbContext.Entry(article).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var article = _appDbContext.Articles.Find(id);

            if (article == null)
            {
                return;
            }

            _appDbContext.Articles.Remove(article);
            _appDbContext.SaveChanges();
        }

        public Article Get(int id)
        {
            return _appDbContext.Articles.Find(id);
        }

        public IEnumerable<Article> GetByChannelTag(string tagName, int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.Articles
                        where p.ChannelTags.Contains(tagName)
                        orderby p.CreatedUtc descending
                        select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.Articles.Count(x => x.ChannelTags.Contains(tagName));
            return list.ToList();
        }

        public IEnumerable<Article> GetByColumnTag(string tagName, int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.Articles
                        where p.ColumnTags.Contains(tagName)
                        orderby p.CreatedUtc descending
                        select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.Articles.Count(x => x.ColumnTags.Contains(tagName));
            return list.ToList();
        }
    }
}
