﻿using MyWallet.Domain.Enums;
using MyWallet.Scraper.Dto;
using MyWallet.Scraper.Interfaces;
using AngleSharp.Dom;
using MyWallet.Scraper.Extensions;

namespace MyWallet.Scraper.Strategies
{
    public class GetStockStrategy : IScraperStrategy
    {
        private readonly IDocument _stock;

        public bool ApplyTo(CategoryType category)
        {
            return category == CategoryType.Stock;
        }

        public ScraperStrategyResponse Execute()
        {
            return new ScraperStrategyResponse(
                GetName(),
                GetCnpj(),
                "",
                GetPrice()
            );
        }

        public string GetCnpj()
        {
            return _stock.GetTextBySelector("#company-section > div:nth-child(1) > div > div.d-block.d-md-flex.mb-5.img-lazy-group > div.company-description.w-100.w-md-70.ml-md-5 > h4 > small");
        }

        public string GetName()
        {
            return _stock.GetTextBySelector("#company-section > div:nth-child(1) > div > div.d-block.d-md-flex.mb-5.img-lazy-group > div.company-description.w-100.w-md-70.ml-md-5 > h4 > span");
        }

        public string GetPrice()
        {
            return _stock.GetTextBySelector("#main-2 > div:nth-child(4) > div > div.pb-3.pb-md-5 > div > div.info.special.w-100.w-md-33.w-lg-20 > div > div:nth-child(1) > strong");
        }

        public static string GetUrl(string ticker)
        {
            return $"https://statusinvest.com.br/acoes/{ticker}";
        }
    }
}

