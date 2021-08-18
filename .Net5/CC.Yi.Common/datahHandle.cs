using CC.Yi.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Yi.Common
{
    public static class datahHandle
    {
        //将children属性null替换成[]
        public static List<article> artData( List<article> data)
        {          
            foreach (var k1 in data)
            {
                if (k1.is_delete == (short)ViewModel.Enum.DelFlagEnum.Deleted)
                {
                    data.Remove(k1);
                    return data;
                }
                k1.content = "";
                if (k1.children == null || k1.children.Count == 0)
                {
                    k1.children = new List<article>();
                }
                else
                {
                    artData(k1.children);
                }
            }
            return data;
        }

        
        //将所有对象平铺
        public static List<article> tileArt(List<article> data)
        {
            List<article> mydata = new List<article>();

            tileArt2(data, mydata);

            return mydata;
        }

        public static void tileArt2(List<article> data, List<article> mydata)
        {
            foreach (var k1 in data)
            {
              
                if (k1.is_delete == (short)ViewModel.Enum.DelFlagEnum.Deleted)
                {
                 
                    continue;
                }
                mydata.Add(new article { id = k1.id, content = k1.content, name = k1.name });

                if (k1.children != null && k1.children.Count != 0)
                {
                    tileArt2(k1.children, mydata);
                }

                
            }

            //for(int i=0;i<data.Count;i++)
            //{
            //    if (data[i].is_delete == (short)ViewModel.Enum.DelFlagEnum.Deleted)
            //    {
            //        data.Remove(data[i]);
            //        return;
            //    }
            //    if (data[i].children != null && data[i].children.Count != 0)
            //    {
            //        tileArt2(data[i].children, mydata);
            //    }

            //    mydata.Add(new article { id = data[i].id, content = data[i].content, name = data[i].name });
            //}
        }
    }
}
