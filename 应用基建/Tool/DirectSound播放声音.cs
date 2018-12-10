/*
 using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;
using Microsoft.DirectX.AudioVideoPlayback;
namespace TestDirectSound
{
    public partial class Form1 : Form
    {
        void Play(String strMusicPath)
        {
            //Create and setup the sound device.
            Device dev = new Device();
            dev.SetCooperativeLevel(this, CooperativeLevel.Normal);

            //Create and setup the buffer description.
            BufferDescription buffer_desc = new BufferDescription();
            buffer_desc.ControlEffects = false; //this has to be true to use effects.
            buffer_desc.GlobalFocus = true; //play sound even if application loses focus.
            
            //Create and setup the buffer for playing the sound.
            Microsoft.DirectX.DirectSound.Buffer buffer = new Microsoft.DirectX.DirectSound.Buffer(
                strMusicPath,
                buffer_desc,
                dev);
            
            buffer.Play(0, BufferPlayFlags.Default);
        }
       void Play2(String str)
        {
            Audio it = new Audio(str);
            it.Play();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Play(@"E:\KuGou\百度云】.wav");
            Play2(@"E:\KuGou\杨幂 - 爱的供养.mp3");
        }
    }
}

 
 */