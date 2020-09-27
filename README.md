# Vendr Product Reviews :star:

Basic functionality to view and manage reviews in Vendr.

Insert the following partial on the product page:

```
@Html.Partial("ProductReviews", new ViewDataDictionary
{
    { "storeId", store.Id },
    { "productReference", Model.GetProductReference() }
})
```

## TODO

- [x] Add example of basic review form on product page.
- [x] Extract reviews for product and calculated average score.
- [x] Add tree nodes for each store in backoffice.
- [x] Add paged list of reviews in backoffice with filter options.
- [ ] Search reviews in specific properties.
- [x] Add page to view and edit some properties on review.
- [x] Allow to delete review(s).
- [ ] Allow to change status og review(s).
- [x] Change review UpdateDate on save.
- [x] Add email field to review.
- [ ] Show product details like sku, name and image on edit page.
- [ ] Recommend this product (yes/no).
- [ ] Verified purchase (yes/no).
- [ ] Votes positive
- [ ] Votes negative
- [ ] Reply/comment on review.
