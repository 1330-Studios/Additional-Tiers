namespace AdditionalTiers.Utils.Components;
[RegisterTypeInIl2Cpp(false)]
internal class Round8Scale : MonoBehaviour {
    public Round8Scale(IntPtr obj0) : base(obj0) { ClassInjector.DerivedConstructorBody(this); }

    public Round8Scale() : base(ClassInjector.DerivedConstructorPointer<Round8Scale>()) { }

    public float Scale;

    public void Start() {
        if (gameObject != null && gameObject.transform.localScale.z != Scale) {
            gameObject.transform.localScale = new Vector3(Scale, Scale, Scale);
        }
    }

    public void Update() {
        if (gameObject != null && gameObject.transform.localScale.z != Scale) {
            gameObject.transform.localScale = new Vector3(Scale, Scale, Scale);
        }
    }
}