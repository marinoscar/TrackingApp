namespace TrackingApp.Droid
{
    public abstract class PresenterBase
    {
        public PresenterBase(IActivity activity)
        {
            Activity = activity;
            BindView();
        }

        public virtual IActivity Activity { get; private set; }

        /// <summary>
        /// Bind the view events to the activity
        /// </summary>
        public abstract void BindView();

    }
}