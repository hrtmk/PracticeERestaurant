using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel;
using eRestaurantSystem.Data.Entities;
using eRestaurantSystem.Data.POCOs;
using eRestaurantSystem.Data.DTOs;
using eRestaurantSystem.DAL;

namespace eRestaurantSystem.BLL
{
    [DataObject]
    public class ItemController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<MenuCategoryFoodItemsPOCO> MenuCategoryFoodItems1_Get()
        {
            using (var context = new eRestaurantContext())
            {
                var results = from food in context.Items
                              group food by new { food.MenuCategory.Description } into tempdataset
                              select new 
                              {
                                  MenuCategoryDescription = tempdataset.Key.Description,
                                  FoodItems = from x in tempdataset
                                              select new MenuCategoryFoodItemsPOCO
                                              {
                                                  ItemID = x.ItemID,
                                                  FoodDescription = x.Description,
                                                  CurrentPrice = x.CurrentPrice,
                                                  //TimesServed = x.B illitems.Count()
                                              }
                              };
                return results.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MenuCategoryFoodItemsDTO> MenuCategoryFoodItems2_Get()
        {
            using (var context = new eRestaurantContext())
            {
                var results = from food in context.Items
                              group food by new { food.MenuCategory.Description } into tempdataset
                              select new MenuCategoryFoodItemsDTO
                              {
                                  MenuCategoryDescription = tempdataset.Key.Description,
                                  FoodItems = (from x in tempdataset
                                              select new FoodItemCounts
                                              {
                                                  ItemID = x.ItemID,
                                                  FoodDescription = x.Description,
                                                  CurrentPrice = x.CurrentPrice,
                                                  TimesServed = 15
                                                  //TimesServed = x.Billitems.Count()
                                              }).ToList()
                              };
                return results.ToList();
            }
        }
    }
}
