Imports System.Net
Imports System.Web
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class Form1
    '' https://cloud.google.com/translate/docs/languages
    Dim country() As String = {"Afrikaans", "Albanian", "Amharic", "Arabic", "Armenian", "Azeerbaijani", "Basque", "Belarusian", "Bengali", "Bosnian", "Bulgarian", "Catalan", "Cebuano", "Chinese (Simplified)", "Chinese (Traditional)", "Corsican", "Croatian", "Czech", "Danish", "Dutch", "English", "Esperanto", "Estonian", "Finnish", "French", "Frisian", "Galician", "Georgian", "German", "Greek", "Gujarati", "Haitian Creole", "Hausa", "Hawaiian", "Hebrew", "Hindi", "Hmong", "Hungarian", "Icelandic", "Igbo", "Indonesian", "Irish", "Italian", "Japanese", "Javanese", "Kannada", "Kazakh", "Khmer", "Korean", "Kurdish", "Kyrgyz", "Lao", "Latin", "Latvian", "Lithuanian", "Luxembourgish", "Macedonian", "Malagasy", "Malay", "Malayalam", "Maltese", "Maori", "Marathi", "Mongolian", "Myanmar (Burmese)", "Nepali", "Norwegian", "Nyanja (Chichewa)", "Pashto", "Persian", "Polish", "Portuguese (Portugal, Brazil)", "Punjabi", "Romanian", "Russian", "Samoan", "Scots Gaelic", "Serbian", "Sesotho", "Shona", "Sindhi", "Sinhala (Sinhalese)", "Slovak", "Slovenian", "Somali", "Spanish", "Sundanese", "Swahili", "Swedish", "Tagalog (Filipino)", "Tajik", "Tamil", "Telugu", "Thai", "Turkish", "Ukrainian", "Urdu", "Uzbek", "Vietnamese", "Welsh", "Xhosa", "Yiddish", "Yoruba", "Zulu"}
    Dim countryCode() As String = {"af", "sq", "am", "ar", "hy", "az", "eu", "be", "bn", "bs", "bg", "ca", "ceb", "zh-CN", "zh-TW", "co", "hr", "cs", "da", "nl", "en", "eo", "et", "fi", "fr", "fy", "gl", "ka", "de", "el", "gu", "ht", "ha", "haw", "he**", "hi", "hmn", "hu", "is", "ig", "id", "ga", "it", "ja", "jw", "kn", "kk", "km", "ko", "ku", "ky", "lo", "la", "lv", "lt", "lb", "mk", "mg", "ms", "ml", "mt", "mi", "mr", "mn", "my", "ne", "no", "ny", "ps", "fa", "pl", "pt", "pa", "ro", "ru", "sm", "gd", "sr", "st", "sn", "sd", "si", "sk", "sl", "so", "es", "su", "sw", "sv", "tl", "tg", "ta", "te", "th", "tr", "uk", "ur", "uz", "vi", "cy", "xh", "yi", "yo", "zu"}

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            For i As Integer = 0 To country.Count - 1
                Me.ListBox1.Items.Add(country(i))
                Me.ListBox2.Items.Add(country(i))
            Next
            Me.ToolStripStatusLabel1.Text = "You Have  " & Me.ListBox1.Items.Count & "  Language"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            TextBox2.Text = gtranslate(TextBox1.Text, countryCode(Me.ListBox1.SelectedIndex), countryCode(Me.ListBox2.SelectedIndex)) 'From italian to english,
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Shared Function gtranslate(ByVal inputtext As String, ByVal fromlangid As String, ByVal tolangid As String) As String
        inputtext = WebUtility.HtmlEncode(inputtext)
        Dim step1 As New WebClient With {
            .Encoding = Encoding.UTF8
        }
        Dim step2 As String = step1.DownloadString("https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=" & tolangid & "&hl=" & fromlangid & "&dt=t&dt=bd&dj=1&source=icon&q=" & inputtext)
        Dim step3 As JObject = JObject.Parse(step2)
        Dim step4 As String = step3.SelectToken("sentences[0]").SelectToken("trans").ToString()
        Return step4
    End Function
End Class