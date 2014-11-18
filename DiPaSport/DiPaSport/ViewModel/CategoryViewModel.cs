using Com.IT.DiPaSport.Model;
using Com.IT.DiPaSport.Model.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Com.IT.DiPaSport.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryViewModel : JSONCallbackListener
    {
        /// <summary>
        /// The list main category
        /// </summary>
        private ObservableCollection<CategoryInfo> ListMainCategory;
        
        /// <summary>
        /// The category callback
        /// </summary>
        public CategoryListener CategoryCallback;

        /// <summary>
        /// The home listener
        /// </summary>
        public HomeActionListener HomeListener;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryViewModel"/> class.
        /// </summary>
        public CategoryViewModel()
        {
            ListMainCategory = new ObservableCollection<CategoryInfo>();
        }

        /// <summary>
        /// 
        /// </summary>
        public interface CategoryListener
        {
            void OnFetchCategory(ObservableCollection<CategoryInfo> categories);
            void OnFetchError();
        }

        /// <summary>
        /// Gets the list category.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<CategoryInfo> fetchListCategories()
        {
            SearchByCategoryModel category = new SearchByCategoryModel() { RegisterCallback = this };
            category.GetListCategories();

            ObservableCollection<CategoryInfo> test = new ObservableCollection<CategoryInfo>();
            CategoryInfo cat1 = new CategoryInfo
            {
                Title = "cat1"
            };

            CategoryInfo cat2 = new CategoryInfo
            {
                Title = "cat2"
            };

            CategoryInfo cat3 = new CategoryInfo
            {
                Title = "cat3"
            };

            test.Add(cat1);
            test.Add(cat2);
            test.Add(cat3);

            return test;
        }

        public override void OnResults(JObject results)
        {
            if (results != null)
            {
                // data received from server
                JArray data = (JArray)results[Constants.JSON_TAG.DATA];
                // init main category
                ListMainCategory = new ObservableCollection<CategoryInfo>();

                // 
                CategoryInfo catInfo;
                foreach (var category in data)
                {
                    string id = (string)category[Constants.JSON_TAG.CAT_ID];

                    if (id.Equals("29") || id.Equals("50")) // Parts, Equipment
                    {
                        string title = (string)category[Constants.JSON_TAG.CAT_TITLE];
                        catInfo = new CategoryInfo()
                        {
                            ID = id,
                            Title = title.Trim()
                        };

                        // Check sub-category
                        JArray subCategories = (JArray)category[Constants.JSON_TAG.CAT_SUBMENU];
                        if (subCategories != null)
                        {
                            catInfo.SubCategories = new ObservableCollection<CategoryInfo>();
                            CategoryInfo subCat;
                            foreach (var subcategory in subCategories)
                            {

                                string sid = (string)subcategory[Constants.JSON_TAG.CAT_ID];
                                if (sid == "22" || sid == "430" || sid == "418") continue;
                                string stitle = (string)subcategory[Constants.JSON_TAG.CAT_TITLE];
                                subCat = new CategoryInfo
                                {
                                    ID = sid,
                                    Title = stitle.Trim()
                                };
                                catInfo.SubCategories.Add(subCat);
                            }
                        }
                        ListMainCategory.Add(catInfo);
                    }
                } // END for

                if (CategoryCallback != null)
                {
                    CategoryCallback.OnFetchCategory(ListMainCategory);
                }
            }
            else
            {
                if (CategoryCallback != null)
                {
                    CategoryCallback.OnFetchError();
                }
            }
        }

        public override void OnErrors(sbyte ErrorCode, string ErrorMessage)
        {
            if (CategoryCallback != null)
            {
                CategoryCallback.OnFetchError();
            }
        }

        public override void StartRequest()
        {
            if (HomeListener != null)
            {
                HomeListener.OnBeginRequestCategory();
            }
        }

        public override void EndRequest()
        {
            if (HomeListener != null)
            {
                HomeListener.OnEndRequestCategory();
            }
        }
    }
}
