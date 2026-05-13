# Picker 3D Klonu — Proje Rehberi

> **Claude için not:** Bu dosyayı her sohbet başında oku. Proje hakkında temel bağlam burada — kullanıcı her sohbette baştan anlatmasın diye yazıldı. Detaylı implementasyon planı: `C:\Users\halil\.claude\plans\bak-unityde-bir-oyun-peaceful-newt.md`

## Proje Özeti
Unity 6 (6000.4.2f1) + URP ile yapılan mobil "Picker 3D" oyunu klonu. Hyper-casual: oyuncu otomatik ileri kayan bir bar'ı (picker) swipe ile sağa-sola hareket ettirir, yoldaki topları önünde iter, kapılardan minimum top sayısıyla geçer, sondaki kamyona dökerek 1-3 yıldız kazanır.

## İş Bölümü
- **Claude (ben):** Tüm C# scriptlerini yazar. Editor adımlarını **tek tek**, numaralı, hangi obje/component/field olduğunu açıkça söyleyerek tarif eder.
- **Kullanıcı (Salih):** Unity Editor'de prefab oluşturma, sahne kurulumu, inspector referansı atama, component ekleme, material/model atama gibi manuel işleri yapar.

## Klasör Yapısı
```
Assets/
  _Project/
    Scripts/
      Core/       (GameManager, LevelManager, InputManager, CameraFollow)
      Player/     (PickerController, PickerCollector)
      Gameplay/   (Collectible, Gate, FailZone, EndRamp, Truck)
      Data/       (LevelData, LevelDatabase — ScriptableObject'ler)
      UI/         (UIManager, StarDisplay)
      Polish/     (CameraShake vs.)
    Prefabs/      (Picker, Collectible, Gate, Truck, LevelPiece)
    Materials/
    Models/
    Audio/
    Data/         (Level_01.asset, LevelDatabase.asset)
    Scenes/       (Game.unity)
```
`Assets/` kökündeki `Scenes/`, `Settings/`, `TutorialInfo/`, `InputSystem_Actions.inputactions`, `Readme.asset` Unity URP template'inden gelen default dosyalar — onlara dokunma.

## Tags & Layers
- **Tags:** `Player`, `Collectible`, `Gate`, `FailZone`, `EndZone`
- **Layers:** `Player`, `Collectible`, `Ground`, `Wall`

## Naming Convention
- Scriptler PascalCase, sınıf adı = dosya adı.
- Private field'lar `[SerializeField] private` + camelCase (`_camelCase` değil, sadece `camelCase`).
- Public property'ler PascalCase.
- Event'ler `OnXxx` (`OnGameStart`, `OnLevelComplete`).
- Prefab dosya adı = ana objenin adı (`Picker.prefab`, `Gate.prefab`).

## Faz Durumu
- [~] Faz 0 — Proje temeli: CLAUDE.md hazır, scriptler yazıldı. Editor tarafı (klasör/tag/layer/sahne kurulumu) **henüz başlamadı**.
- [ ] Faz 1 — Core scriptleri yazıldı (GameManager, LevelManager, InputManager, CameraFollow). Editor kurulumu yapılacak.
- [ ] Faz 2 — Player scriptleri yazıldı (PickerController, PickerCollector). Editor kurulumu yapılacak.
- [ ] Faz 3 — Gameplay scriptleri yazıldı (Collectible, Gate, FailZone, EndRamp, Truck). Editor kurulumu yapılacak.
- [ ] Faz 4 — Data scriptleri yazıldı (LevelData, LevelDatabase). Asset oluşturma yapılacak.
- [ ] Faz 5 — UI scriptleri yazıldı (UIManager, StarDisplay). Canvas kurulumu yapılacak.
- [ ] Faz 6 — Polish (CameraShake, particles, audio — opsiyonel)

> **Not:** Tüm scriptler v1 olarak hazır. Kullanıcı Editor tarafında prefab/sahne/asset kurulumuna **henüz başlamadı** — Faz 1'in editor adımlarından başlanacak. Her faz sonu kabul kriteri tatmin edilmeden sonraki faza geçilmiyor.

## Teknik Notlar
- **Input:** Unity Input System paketi kurulu, ama prototipte basit `Pointer.current` API kullanılıyor (mouse + touch hem editörde hem mobilde çalışır).
- **Render:** URP 17.4.0. Materyaller `Universal Render Pipeline/Lit` shader'ı kullanmalı.
- **Physics:** Default gravity (-9.81). Picker Rigidbody'sinin rotasyonu tüm eksenlerde kilitli (FreezeRotation X/Y/Z).
- **TMP:** İlk Canvas oluşturulduğunda "Import TMP Essentials" diyalogu çıkar; kabul et.
- **Singleton pattern:** Manager'lar sahne içinde yaşar, `DontDestroyOnLoad` kullanılmaz (tek sahne yeterli).

## Convention'lar — Claude bunlara uy
- Kullanıcı Türkçe konuşur, sen de Türkçe cevapla.
- Editor talimatları **numaralı liste** halinde, kısa ve net olsun. "Hierarchy'de sağ tık → Create Empty → adı 'GameManager'" gibi.
- Inspector referans atama talimatı her zaman: "GameManager objesini seç → Inspector'da `levelManager` field'ına LevelManager objesini sürükle".
- Bir fazı bitirmeden bir sonrakine geçme. Faz sonu kabul kriteri tatmin edilince devam et.
- Kodda gereksiz yorum yazma. WHY açıklaması gerekiyorsa tek satır yaz.
- `using` blokları minimum olsun, sadece kullanılan namespace'ler.

## Test/Verification
- Editor Play Mode'da test et, Console'da hata olmamalı.
- Faz sonlarında plan dosyasındaki "Kabul Kriterleri" tatmin olmalı.
- Mobil build (Android) son fazda yapılacak — şimdilik sadece editör.

## Referanslar
- Detaylı plan: `C:\Users\halil\.claude\plans\bak-unityde-bir-oyun-peaceful-newt.md`
- Unity sürümü: 6000.4.2f1
- URP: 17.4.0
- Input System: 1.19.0
