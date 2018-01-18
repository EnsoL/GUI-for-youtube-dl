using System;
using System.Text;

namespace youtube_dl_gui
{
    class CLIString
    {
        static string START_OF_COMMAND = "youtube-dl --newline ";
        static string PLACE_HOLDER = "***This*should*be*replaced~***";

        private string urlString;
        private bool hasUrl;
        private bool hasPlaceholder;
        private Uri url;
        private StringBuilder command;

        public CLIString()
        {
            urlString = "";
            url = null;
            hasUrl = false;
            hasPlaceholder = false;
            command = null;
        }

        public CLIString(string urlString)
        {
            this.urlString = urlString;
            hasPlaceholder = false;
            hasUrl = createUrl();
            createStartOfCommand();
        }

        public CLIString(Uri url)
        {
            urlString = "";
            this.url = url;
            hasPlaceholder = false;
            hasUrl = url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps;
            createStartOfCommand();
        }

        public CLIString(CLIString arg)
        {
            this.urlString = arg.urlString;
            this.url = arg.url;
            this.hasPlaceholder = arg.hasPlaceholder;
            this.hasUrl = arg.hasUrl;
            this.command = arg.command;
        }

        public bool createStartOfCommand()
        {
            if (!hasUrl) if (!createUrl())
                {
                    command = null;
                    return false;
                }
            command = new StringBuilder(START_OF_COMMAND + url.OriginalString + " ");
            hasPlaceholder = false;
            return true;
        }

        public void createPlaceholderStartOfCommand()
        {
            command = new StringBuilder(START_OF_COMMAND + PLACE_HOLDER + " ");
            hasPlaceholder = true;
        }

        private bool createUrl()
        {
            if (Uri.IsWellFormedUriString(urlString, UriKind.Absolute))
            {
                url = new Uri(urlString);
                if (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps)
                {
                    hasUrl = true;
                    if (hasPlaceholder) replacePlaceholder();
                    return true;
                }
                else urlString = "";
            }

            url = null;
            return false;
        }

        private bool replacePlaceholder()
        {
            if (hasUrl && hasPlaceholder)
            {
                command.Replace(PLACE_HOLDER, url.OriginalString);
                hasPlaceholder = false;
                return true;
            }
            else return false;
        }

        public bool addUrl(string url)
        {
            if (!hasUrl)
            {
                urlString = url;
                return createUrl();
            }
            else return false;
        }

        public bool addUrl(Uri url)
        {
            if (!hasUrl)
            {
                if (url.IsAbsoluteUri && (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps))
                {
                    this.url = url;
                    urlString = url.AbsolutePath;
                    hasUrl = true;
         
                    if (hasPlaceholder) replacePlaceholder();

                    return true;
                }
                else return false;
            }
            else return false;
        }

        public bool isAtleastABaseCommand()
        {   // This method checks if this is atleast a basic request
            // which means that it needs to have youtube-dl --newline URL
            return (hasUrl && !hasPlaceholder && command != null);
        }

        public bool addFileFormat(string format)
        {
            if (command == null) if (!createStartOfCommand()) return false;
            if (format == null) return false;

            switch (format.Trim().ToLower())
            {
                case "audio": return addOption("-x ");
                case "mp3": return addOption("-x --audio-format mp3 ");
                case "m4a": return addOption("-x --audio-format m4a ");
                case "flac": return addOption("-x --audio-format flac ");
                case "aac": return addOption("-x --audio-format aac ");
                case "wav": return addOption("-x --audio-format wav ");
                case "vorbis": return addOption("-x --audio-format vorbis ");
                case "opus": return addOption("-x --audio-format opus ");
                case "video": return true;
                case "mp4": return addOption("-f mp4 ");
                case "webm": return addOption("-f webm ");
                case "3gp": return addOption("-f 3gp ");
            }

            return true;
        }

        public bool addDownloadLocation(string location)
        {
            return addOption("-o " + location.Trim() + "%(title)s.%(ext)s ");
        }

        public bool addKeepBoth()
        {
            return addOption("-k ");
        }

        public bool addGeoBypass()
        {
            return addOption("--geo-bypass ");
        }

        public bool addThumbnail()
        {
            return addOption("--write-thumbnail ");
        }

        public bool addWriteSubs()
        {
            return addOption("--write-sub ");
        }

        public bool addWriteAutoSubs()
        {
            return addOption("--write-auto-sub ");
        }

        private bool addOption(string option)
        {
            if (command != null)
            {
                command.Append(option);
                return true;
            } else if (createStartOfCommand())
            {
                command.Append(option);
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            if (command != null) return command.ToString();
            else return "empty command";
        }
    }
}
