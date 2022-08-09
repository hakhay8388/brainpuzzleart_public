using Base.Data.nDataFileEntity;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Base.FileData;
using Base.FileData.nFileDataService;
using Core.BatchJobService.nBatchJobManager.nJobs.nTestJob;
using Core.BatchJobService.nDataService.nDataManagers;
using Core.BatchJobService.nDefaultValueTypes;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Data.GenericWebScaffold.nGlobalDataServices;
using Integration.Managers.nManagers;
using Integration.MicroServiceGraph.nMicroService;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Core.BatchJobService.nBatchJobManager.nJobs.nSitemapGenerateJob
{
    public class cSitemapGenerateJob : cBaseJob<cSitemapGenerateJobProps>
    {
        cBatchJobExecutionDataManager BatchJobExecutionDataManager { get; set; }
        cLanguageDataManager LanguageDataManager { get; set; }
        public cSitemapGenerateJob(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IManagers _Managers, IMicroService _MicroService, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
            , cBatchJobDataManager _BatchJobDataManager
            , cBatchJobExecutionDataManager _BatchJobExecutionDataManager
            , cLanguageDataManager _LanguageDataManager
            )
         : base(BatchJobIDs.SitemapGenerateJob, _CoreServiceContext, _Managers, _MicroService, _DataServiceManager, _FileDataService, _BatchJobDataManager)
        {
            BatchJobExecutionDataManager = _BatchJobExecutionDataManager;
            LanguageDataManager = _LanguageDataManager;
        }

        public override cBatchJobResult Run(cSitemapGenerateJobProps _JobProps)
        {
            cBatchJobResult __BatchJobResult = new cBatchJobResult("");
            cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
            List<string> __SitemapUrls = new List<string>();



            //__SitemapUrls.Add(PageIDs.SellerRegisterPage.Url);
            //__SitemapUrls.Add(PageIDs.CreditPackageListPage.Url);


            for (int i = 0; i < __SitemapUrls.Count; i++)
            {
                __SitemapUrls[i] = __SitemapUrls[i].Replace("&", "&amp;");
            }



            List<List<string>> __SiteMaps = __SitemapUrls.SplitList(5000);
            SitemapFileGenerate(__SiteMaps);
            return __BatchJobResult;
        }

        private void SitemapFileGenerate(List<List<string>> __SiteMaps)
        {

            cGlobalDataService __GlobalDataService = (cGlobalDataService)DataServiceManager.GetGlobalDataService();
            string __Host = __GlobalDataService.GetProxyedSiteUrl();

            cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
            string __DateAll = DateTime.Now.ToString("s") + "+02:00";

            StringBuilder __Builder = new StringBuilder();
            string __HeaderXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>";
            __HeaderXml += Environment.NewLine + @"<urlset xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd http://www.w3.org/TR/xhtml11/xhtml11_schema.html http://www.w3.org/2002/08/xhtml/xhtml1-strict.xsd"" xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"" xmlns:xhtml=""http://www.w3.org/TR/xhtml11/xhtml11_schema.html"">";
            string __FooterXml = "</urlset>";

            __Builder.Append(__HeaderXml);

            cGeneralConfigEntity __GeneralConfig = ((cFileDataService)FileDataService).FindByID<cGeneralConfigEntity>(1);
            List<string> __SiteMapFileNames = new List<string>();
            App.Handlers.FileHandler.MakeDirectory(__GeneralConfig.WebSitePath + "\\sitemap", true);
            for (int i = 0; i < __SiteMaps.Count; i++)
            {
                string __SiteMapName = "sitemap" + (i + 1).ToString() + ".xml";
                string __SiteMapFileName = "\\sitemap\\" + __SiteMapName;

                __Builder.Clear();
                __Builder.AppendLine(__HeaderXml);
                List<cLanguageEntity> __Languages = LanguageDataManager.GetLanguages();
                for (int k = 0; k < __SiteMaps[i].Count; k++)
                {

                    string __Url = __SiteMaps[i][k].ToString();
                    List<string> __Xhtmls = new List<string>();
                    foreach (cLanguageEntity __Language in __Languages)
                    {
                        List<cLanguageHrefLangEntity> __HrefLangs = __Language.HrefLangs.ToList();

                        foreach (cLanguageHrefLangEntity __HrefLang in __HrefLangs)
                        {
                            List<string> __SplitedUrls = new List<string>();
                            __SplitedUrls.AddRange(__Url.Split("/"));
                            __SplitedUrls.Remove("");
                            for (int __SplitedId = 0; __SplitedId < __SplitedUrls.Count; __SplitedId++)
                            {
                                try
                                {
                                    PageIDs __PageID = PageIDs.GetByUrl(__SplitedUrls[__SplitedId]);
                                    if (__PageID != null)
                                    {
                                        string __NewUrl = App.Handlers.LanguageHandler.GetWordValue(__Language.Code, "PageRoute_" + __PageID.OriginalCode);

                                        if (__NewUrl != null && __NewUrl != "" && !__NewUrl.Contains("dilinde"))
                                        {
                                            __SplitedUrls[__SplitedId] = __NewUrl;
                                        }

                                    }

                                }
                                catch (Exception __Ex)
                                {

                                }
                            }

                            string __XhtmlString = string.Join("/", __SplitedUrls);
                            __XhtmlString = @"<xhtml:link rel=""alternate"" hreflang=""" + __HrefLang.Code + @""" href=""https://" + __Host + "/" + __Language.Code + "/" + __XhtmlString + @""" />";
                            __Xhtmls.Add(__XhtmlString);

                        }

                    }
                    __Xhtmls.Add(@"<xhtml:link rel=""alternate"" hreflang=""x-default"" href =""" + "https://" + __Host + "/" + __Url + @""" />");
                    string __WriteXmlString = "<url>" + Environment.NewLine + "<loc>" + "https://" + __Host + "/" + __Url + "</loc>" + string.Join(Environment.NewLine, __Xhtmls) + Environment.NewLine + "<lastmod>" + __DateAll + "</lastmod>" + Environment.NewLine + "</url>";
                    __Builder.Append(__WriteXmlString);
                }
                __Builder.AppendLine(__FooterXml);

                if (File.Exists(__GeneralConfig.WebSitePath + __SiteMapFileName))
                {
                    File.Delete(__GeneralConfig.WebSitePath + __SiteMapFileName);
                }

                App.Handlers.FileHandler.WriteString(@"<?xml version=""1.0"" encoding=""UTF-8""?>" + Environment.NewLine + PrettyXml(__Builder.ToString()), __GeneralConfig.WebSitePath + __SiteMapFileName);

                __SiteMapFileNames.Add(__SiteMapName);
            }

            string __SiteMapIndexContent = @"<?xml version=""1.0"" encoding=""UTF-8""?><sitemapindex xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"">";


            string __SiteMapIndexPath = "\\sitemap-index.xml";
            for (int i = 0; i < __SiteMapFileNames.Count; i++)
            {
                string __FileName = "/sitemap/" + __SiteMapFileNames[i];

                __SiteMapIndexContent += "<sitemap><loc>" + __DataService.SiteUrl + __FileName + "</loc><lastmod>" + __DateAll + "</lastmod></sitemap>";
            }
            __SiteMapIndexContent += "</sitemapindex>";
            if (File.Exists(__GeneralConfig.WebSitePath + __SiteMapIndexPath))
            {
                File.Delete(__GeneralConfig.WebSitePath + __SiteMapIndexPath);
            }

            App.Handlers.FileHandler.WriteString(__SiteMapIndexContent, __GeneralConfig.WebSitePath + __SiteMapIndexPath);
        }
        public static string PrettyXml(string _Xml)
        {
            StringBuilder __StringBuilder = new StringBuilder();

            var __Element = XElement.Parse(_Xml);

            XmlWriterSettings __Settings = new XmlWriterSettings();
            __Settings.OmitXmlDeclaration = true;
            __Settings.Indent = true;
            __Settings.NewLineOnAttributes = false;

            using (XmlWriter __XmlWriter = XmlWriter.Create(__StringBuilder, __Settings))
            {
                __Element.Save(__XmlWriter);
            }

            return __StringBuilder.ToString();
        }
    }
}
