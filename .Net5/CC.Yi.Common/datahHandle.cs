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
                if (k1.children != null && k1.children.Count != 0)
                {
                    tileArt2(k1.children, mydata);
                }
               
                mydata.Add(new article { id=k1.id,content=k1.content,name=k1.name});
            }
        }
    }
}
