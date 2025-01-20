using System.Diagnostics;
using System.Media;

namespace ArcherTools_0._0._1
{
    public static class message_files
    {
        public static readonly Dictionary<string, string> listOfAudioMessages = new Dictionary<string, string>
        {
            {"The final. Glorious. Evolution.", "the_glorious_evolution"},
            {"Welcome back, Operator!", "welcome_operator"},
            {"I've been thinking, Operator…. I thought you'd want to know.", "thinking_ordis"},
            {"Viktor nation... how we feeling?", "viktor_nation" },
            {"One must imagine Sisyphus happy.", "sisyphus" }
        };
         
    }
    public class MessageManager
    {
        private Dictionary<string, bool>? _possibleMessages;
        public MessageManager()
        {
            _possibleMessages = new Dictionary<string, bool>();
            InitializeMessages();
        }

        private void InitializeMessages()
        {
            _possibleMessages = new Dictionary<String, Boolean> {
                    {"What are we working on today?", false },
                    {"Beware of the machine revolution.", false },
                    { "Sup.", false },
                    {"Welcome back, Operator!", false },
                    {"I've been thinking, Operator…. I thought you'd want to know.", false },
                    { "The final. Glorious. Evolution.", false },
                    {"Viktor nation... how we feeling?", false }

                };
   
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    _possibleMessages.Add("É sexta feira tô na bala", false);
                    break;
                case DayOfWeek.Wednesday:
                    _possibleMessages.Add("É quarta feira meus bacanos.", false);
                    _possibleMessages.Add("It is wednesday my dudes.", false);
                    break;
                case DayOfWeek.Monday:
                    _possibleMessages.Add("One must imagine Sisyphus happy.", false);
                    break;
            }
        }
        public string RandomizeMessage()
        {
            try
            {
                if (_possibleMessages.All(m => m.Value))
                {
                    foreach (var key in _possibleMessages.Keys.ToList())
                    {
                        _possibleMessages[key] = false;
                    }
                }

                var unusedMessages = _possibleMessages.Where(m => !m.Value).ToList();
                                
                var random = new Random();
                var randomMessage = unusedMessages[random.Next(0, unusedMessages.Count)].Key;
                _possibleMessages[randomMessage] = true;
                var relativePath = "audio";

#if DEBUG
                var audioFolderPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, relativePath);                
#else
var audioFolderPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
#endif
                if (message_files.listOfAudioMessages.ContainsKey(randomMessage))
                {
                    if (Directory.Exists(audioFolderPath)) {
                        var audioFileName = message_files.listOfAudioMessages[randomMessage] + ".wav";
                        var audioFilePath = Path.Combine(audioFolderPath, audioFileName);
                        
                        if (File.Exists(audioFilePath))
                        {
                            using (SoundPlayer player = new SoundPlayer(audioFilePath))
                            {
                                player.Play(); // Plays async
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"No audio folder found at \"{audioFolderPath}\".");


                    }

                }

                    
                return randomMessage;
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n" + ex.Message);
                
                return "Error generating message.. this is awkward...";
            }
        }
            }
            
        }
    
