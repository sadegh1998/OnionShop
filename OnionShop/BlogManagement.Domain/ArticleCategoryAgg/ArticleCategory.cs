﻿using _0_Framework.Domain;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int ShowOrder { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }

        public ArticleCategory(string name, string description, int showOrder, string picture, string pictureAlt, string pictureTitle, string slug, string keywords, string metaDescription, string canonicalAddress)
        {
            Name = name;
            Description = description;
            ShowOrder = showOrder;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }

        public void Edit(string name, string description, int showOrder, string picture, string pictureAlt, string pictureTitle, string slug, string keywords, string metaDescription, string canonicalAddress)
        {
            Name = name;
            Description = description;
            ShowOrder = showOrder;
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }
    }
}
