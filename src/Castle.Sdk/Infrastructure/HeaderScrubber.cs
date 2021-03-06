﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Castle.Infrastructure
{
    internal static class HeaderScrubber
    {
        static readonly string[] _defaultWhitelist = new[] { "User-Agent" };
        static readonly string[] _defaultBlacklist = new[] { "Cookie", "Authorization" };

        public static IDictionary<string, string> Scrub(
            IDictionary<string, string> headers,
            string[] whitelist,
            string[] blacklist)
        {
            return headers
                .Select(header => Scrub(whitelist, blacklist, header))
                .ToDictionary(x => x.Key, y => y.Value);
        }

        private static KeyValuePair<string, string> Scrub(
            string[] whitelist, 
            string[] blacklist, 
            KeyValuePair<string, string> header)
        {
            // Scrub to "true" so the custom JsonConverter can find it and convert to actual boolean
            const string scrubValue = "true";

            if (blacklist != null && (blacklist.Contains(header.Key, StringComparer.OrdinalIgnoreCase) || _defaultBlacklist.Contains(header.Key, StringComparer.OrdinalIgnoreCase)))
            {
                return new KeyValuePair<string, string>(header.Key, scrubValue);
            }

            if (whitelist != null && whitelist.Length > 0 && (!whitelist.Contains(header.Key, StringComparer.OrdinalIgnoreCase) && !_defaultWhitelist.Contains(header.Key, StringComparer.OrdinalIgnoreCase)))
            {
                return new KeyValuePair<string, string>(header.Key, scrubValue);
            }

            return new KeyValuePair<string, string>(header.Key, header.Value);
        }
    }
}
