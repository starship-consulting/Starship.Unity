namespace Starship.Unity.Tweening
{
	internal interface ITweenValue
	{
		void TweenValue(float floatPercentage);
		bool ignoreTimeScale { get; }
		float duration { get; }
		TweenEasing easing { get; }
		bool ValidTarget();
		void Finished();
	}
}