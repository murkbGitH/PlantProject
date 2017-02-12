﻿using System.Linq;
using System.Text;


/// <summary>
/// 列挙型情報
/// </summary>
public class EnumInfo : ContentInfoBase
{           
    public override StringBuilder BuildScriptText(PlantUMLConvertOption option)
    {
        var builder = new StringBuilder ();
        var tab = string.Empty;
        int tab_num = 0;

        // ネームスペース開始チェック
        if (!string.IsNullOrEmpty (namespaceName)) {
            builder.AppendLine (string.Format ("namespace {0}", namespaceName));
            builder.AppendLine ("{");
            tab_num++;
        }

        // 列挙型定義開始
        tab = StringBuilderSupporter.SetTab (tab_num);
        builder.AppendLine (tab + GetDeclarationName ());
        builder.AppendLine (tab + "{");
        tab_num++;
        {
            tab = StringBuilderSupporter.SetTab (tab_num);

            foreach (var name in GetDeclarationMemberNames ()) {
                builder.AppendLine (tab + name);
            }
        }
        tab_num--;
        tab = StringBuilderSupporter.SetTab (tab_num);
        builder.AppendLine (tab + "}");

        // ネームスペース終了チェック
        if (!string.IsNullOrEmpty (namespaceName)) {
            builder.AppendLine ("}");
        }

        return builder;
    }

    /// <summary>
    /// 宣言する名前
    /// </summary>
    private string GetDeclarationName()
    {
        return string.Format ("public enum {0}", contentName);
    }

    /// <summary>
    /// 宣言するメンバ名
    /// </summary>
    private string[] GetDeclarationMemberNames()
    {
        return memberList.Select (x => x.name.IndexOf (",") >= 0 ? x.name : x.name + ",").ToArray ();
    }
}
