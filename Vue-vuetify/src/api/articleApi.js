import myaxios from '@/utils/myaxios'
export default {
    getArticlesByDiscussId(discussId) {
        return myaxios({
            url: `/Article/GetArticlesByDiscussId?discussId=${discussId}`,
            method: 'get'
        })
    },
    getArticleById(articleId) {
        return myaxios({
            url: `/Article/getArticleById?articleId=${articleId}`,
            method: 'get'
        })

    },

    getArticles() {
        return myaxios({
            url: '/Article/getArticles',
            method: 'get'
        })
    },
    addArticle(article, discussId) {
        return myaxios({
            url: `/Article/addArticle?discussId=${discussId}`,
            method: 'post',
            data: article
        })
    },
    updateArticle(Article) {
        return myaxios({
            url: '/Article/UpdateArticle',
            method: 'post',
            data: Article
        })
    },
    delArticleList(Ids) {
        return myaxios({
            url: '/Article/DelArticleList',
            method: 'post',
            data: Ids
        })
    },
}