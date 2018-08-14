﻿using KNDBsys.Model.BaseInfo;
using KNDBsys.Model.ViewModel.WebVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Common.TreeNode
{
    public class TreeNodeHelper
    {
        public string InitTreeNode_json(List<Authority> treelist,int fid)
        {
            List<WebTreeNode> webTreeNodes = new List<WebTreeNode>();
            List<Authority> fAu_ls = treelist.Where(au => au.ParentID == fid).ToList();
            foreach (var tn in fAu_ls)
            {
                WebTreeNode wtn = new WebTreeNode {
                    id = tn.id,
                    iconCls = tn.Imageid,
                    text = tn.TreeName,
                    children = GetChild(treelist, tn.id)
                };
                webTreeNodes.Add(wtn);
            }
            return DataSwitch.DataToJson(webTreeNodes);
        }

        private object GetChild(List<Authority> treelist, int id)
        {
            List<WebTreeNode> wtnls = new List<WebTreeNode>();
            List<Authority> sonAu_ls = treelist.Where( au => au.ParentID==id & au.AuthTypeID !=6).ToList();
            foreach (var au in sonAu_ls)
            {

                WebTreeNode wtn = new WebTreeNode
                {
                    id = au.id,
                    iconCls = au.Imageid,
                    text = au.TreeName,
                    children = GetChild(treelist, au.id),
                    attributes = CreateUrl(au)
                };
                wtnls.Add(wtn);
            }

            return wtnls;
        }

        private Dictionary<string, string> CreateUrl(Authority auth)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "url", auth.Path }
            };
            return dic;
        }
    }
}
