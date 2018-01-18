using System;
using System.Text;

namespace youtube_dl_gui
{
    class CLIString
    {
        static string START_OF_COMMAND = "youtube-dl ";

        private string urlString;
        private bool hasUrl;
        private Uri url;
        private StringBuilder command;

        CLIString()
        {
            urlString = "";
            url = null;
            hasUrl = false;
            command = null;
        }

        CLIString(string urlString)
        {
            this.urlString = urlString;
            hasUrl = createUrl();
            createStartOfCommand();
        }

        CLIString(Uri url)
        {
            urlString = "";
            this.url = url;
            hasUrl = url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps;
            createStartOfCommand();
        }

        public bool createStartOfCommand()
        {
            if (!hasUrl) if (!createUrl())
            {
                command = null;
                return false;
            }
            command = new StringBuilder(START_OF_COMMAND + url.OriginalString);
            return true;
        }

        public bool addFileFormat(string format)
        {
            if (command == null) if (!createStartOfCommand()) return false;

            if (format != null) switch (format)
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
                    case "mp4": return addOption("--audio-format mp4 ");
                    case "webm": return addOption("--audio-format webm ");
                    case "3gp": return addOption("--audio-format 3gp ");
                }
            else return false;

            return true;
        }

        public bool addKeepBoth()
        {
            return addOption("-k ");
        }

        public bool addDownloadLocation(string location)
        {
            return addOption("-o " + location + "%(title)s.%(ext)s ");
        }

        public bool addGeoBypass()
        {
            return addOption("--geo-bypass ");
        }

        public bool addThumbnail()
        {
            return addOption("--write-thumbnail ");
        }

        private bool addOption(string option)
        {
            if(command != null)
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

        private bool createUrl()
        {
            if (Uri.IsWellFormedUriString(urlString, UriKind.Absolute))
            {
                url = new Uri(urlString);
                if (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps) return true;
            }

            url = null;
            return false;
        }
    }
}
