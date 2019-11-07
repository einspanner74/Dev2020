using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.ComponentModel;


namespace CustomControl
{
    /// <summary>
    /// Repeat button WinForms control. Behaves exactly like standard WinForms button with additional 
    /// repeater functionality: when button is pressed and hold, after <c>InitialDelay</c> button starts 
    /// emitting <c>MouseUp</c> event with <c></c>
    /// </summary>
    public class RepeatButton : Button
    {
        #region Private members

        private Timer m_timerRepeater;      //timer to measure repeat intervals wait.
        private IContainer m_components;    //Components collection of this control (timer)
        private bool m_disposed = false;    //flag used to prevent multiple disposing in Dispose method
        private MouseEventArgs m_mouseDownArgs = null;  //muse down arguments; used by timer when repeating events.

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        public RepeatButton()
            : base()
        {
            InitializeComponent();
            InitialDelay = 400;
            RepeatInterval = 62;
        }

        #region Public properties

        /// <summary>
        /// Initial delay. Time in milliseconds between button press and first repeat action.
        /// </summary>
        [DefaultValue(400)]
        [Category("Enhanced")]
        [Description("Initial delay. Time in milliseconds between button press and first repeat action.")]
        public int InitialDelay { set; get; }

        /// <summary>
        /// Repeat Interval. Repeat between each repeat action while button is hold pressed.
        /// </summary>
        [DefaultValue(62)]
        [Category("Enhanced")]
        [Description("Repeat Interval. Repeat between each repeat action while button is hold pressed.")]
        public int RepeatInterval { set; get; }

        #endregion

        private void InitializeComponent()
        {
            this.m_components = new System.ComponentModel.Container();
            this.m_timerRepeater = new System.Windows.Forms.Timer(this.m_components);
            this.SuspendLayout();
            this.m_timerRepeater.Tick += new System.EventHandler(this.timerRepeater_Tick);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Initiates timer, that issues <c>MouseUp</c> event every <c>RepeatIteral</c> milliseconds. For the first time 
        /// event is fires after <c>InitialDelay</c> milliseconds.
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            //Save arguments
            m_mouseDownArgs = mevent;
            m_timerRepeater.Enabled = false;
            timerRepeater_Tick(null, EventArgs.Empty);
        }

        /// <summary>
        /// Repeat loop happens in thin event handler handler using the following logic:
        /// If handler is called for the first time, it fires <c>MouseDown</c> event and waits <c>InitialDelay</c>
        /// milliseconds till next iteration. Every next iteration is called with delay of <c>RepeatDelay</c>
        /// milliseconds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRepeater_Tick(object sender, EventArgs e)
        {

            base.OnMouseDown(m_mouseDownArgs);
            if (m_timerRepeater.Enabled)
                m_timerRepeater.Interval = RepeatInterval;
            else
                m_timerRepeater.Interval = InitialDelay;

            m_timerRepeater.Enabled = true;

        }

        /// <summary>
        /// Disables timer and repetitions.
        /// </summary>
        /// <param name="mevent"></param>
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            m_timerRepeater.Enabled = false;
        }

        /// <summary>
        /// Disposes local resources (timer).
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    if (m_components != null)
                    {
                        m_components.Dispose();
                    }
                    m_timerRepeater.Dispose();
                }

                m_disposed = true;
                base.Dispose(disposing);
            }
        }
    }

    #region SandCastle help utility requires existence of this empty class
    /* The following empty class exists only in order to provide SandCastle help utility
     * extract namespace summary.   
     */

    /// <summary>
    /// Collection of WinForms components and controls.
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc
    {

    }
    #endregion

}
