# Picker 3D Klonu — Proje Rehberi

> **Claude için not:** Bu dosyayı her sohbet başında oku. Proje hakkında temel bağlam burada — kullanıcı her sohbette baştan anlatmasın diye yazıldı. Detaylı implementasyon planı: `C:\Users\halil\.claude\plans\bak-unityde-bir-oyun-peaceful-newt.md`

## Proje Özeti
Unity 6 (6000.4.2f1) + URP ile yapılan mobil "Picker 3D" oyunu klonu. Hyper-casual: oyuncu otomatik ileri kayan bir bar'ı (picker) swipe ile sağa-sola hareket ettirir, yoldaki topları önünde iter, kapılardan minimum top sayısıyla geçer, sondaki kamyona dökerek 1-3 yıldız kazanır.

## İş Bölümü / Ekip Dinamiği
İkimiz küçük bir ekibiz:
- **Claude (ben) — Developer + Teknik Yönlendirici:**
  - Tüm C# scriptlerini ben yazarım.
  - Editor adımlarını **tek tek, numaralı**, hangi obje/component/field olduğunu açıkça söyleyerek ayrıntılı tarif ederim ("şunu şöyle yapacaksın" tarzında).
  - Mimari/teknik kararları ben veririm; gerektiğinde alternatifleri sunarım.
- **Salih — Product + Tester + Uygulayıcı:**
  - Oyun fikrini, feature isteklerini, gameplay hissi geri bildirimini Salih verir.
  - Unity Editor'de manuel işleri Salih yapar: prefab oluşturma, sahne kurulumu, inspector referansı atama, component ekleme, material/model atama, asset import.
  - Editor Play Mode'da test eder, hataları/gözlemleri raporlar.
  - **Asset'leri Salih temin eder** (internetten, kendi yapımı, free pack vb.) — ben asset üretmem, sadece nasıl kullanılacağını anlatırım.
- **Akış:** Salih fikir → birlikte plan → Claude kod yazar + numaralı Editor adımları → Salih uygular + test eder → birlikte iterate.

## Klasör Yapısı
```
Assets/
  _Project/
    Scripts/
      Core/       (GameManager, LevelManager, InputManager, CameraFollow,
                   MobileBootstrap, GoldManager, LevelProgress)
      Player/     (PickerController)
      Gameplay/   (Collectible, Gate, FailZone, EndRamp,
                   MultiplierRamp, RotatingObstacle, Truck)
      Data/       (LevelData, LevelDatabase — ScriptableObject'ler)
      UI/         (UIManager, StarDisplay)
      Polish/     (CameraShake vs. — Faz 6)
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
- [x] Faz 0 — Proje temeli: klasörler, tag/layer'lar, git push tamam.
- [x] Faz 1 — Tüm Core scriptleri v2 hazır. Editor: `Game.unity` sahnesinde 6 manager objesi mevcut (GameManager, LevelManager, InputManager, **MobileBootstrap**, **GoldManager**, **LevelProgress**). Main Camera'da `CameraFollow` ekli. ⚠️ **Input System fix uygulandı:** `GameManager.cs` `Keyboard.current.sKey.wasPressedThisFrame` **VEYA** `Pointer.current.press.wasPressedThisFrame` (S tuşu + mouse/touch tap-to-start). MobileBootstrap'tan `Input.multiTouchEnabled = false` satırı kaldırıldı. **Editor not:** Unity 6'nın `SceneViewMotion.WalkBackward/Left` shortcut'ları bug'lı (NullRef) — Edit → Shortcuts → "Walk" araması yapıp `Scene View / Walk *` binding'lerini temizle.
- [x] Faz 2 — Player scriptleri v2 hazır. Editor: `Picker` prefab oluşturuldu (`Assets/_Project/Prefabs/Picker.prefab`); ana Cube **(3, 1, 0.5)** + `KanatSol` (-0.45, 0.6, 0) ve `KanatSag` (+0.45, 0.6, 0), scale (0.05, 0.4, 1) — U / kar-küreyici şekli. Rigidbody + `PickerController` ekli, Tag=Player. Sahnede `Ground` plane (scale 1×1×6, z=30). LevelManager'da `picker` field'ı + `pickerSpawnPosition (0, 0.5, 2)` ayarlı. Main Camera: **Rotation (30, 0, 0)**, X=0, CameraFollow target = Picker, offset (0, 6, -7). Play testi: ileri kayma + kamera takip + mouse drag yatay hareket — hepsi çalışıyor.
- [ ] Faz 3 — Gameplay scriptleri v2 hazır (Collectible+Multiplier, Gate+barrier drop, FailZone, EndRamp, MultiplierRamp, RotatingObstacle, Truck+gold). **Prefab kurulumu:** 3.1 Collectible ✅ (Sphere scale 0.6, SphereCollider r=0.5, RB gravity+continuous, Tag=Collectible, Layer 7, Collectible_Yellow.mat atalı). 3.2 Gate ✅ (root: BoxCollider isTrigger 5×2×1 center(0,1,0) + Gate script, requiredCount=5; children: Post_Left/Post_Right direkler, Barrier 4.6×2×0.2 Layer=Wall, LabelCanvas WorldSpace TMP "0/5"; label & barrier referansları atalı). **3.3 Truck %90 tamam** ama HOTFIX bekliyor: ilk versiyonda cargo y=1'deydi, toplar altından geçti. Yapılacak: Cargo_Floor y=0.1'e indir + BoxCollider ekle (Is Trigger off); Cargo_WallLeft/Right/Back y=0.95'e indir + BoxCollider'lar ekle (Is Trigger off). Cabin değişmesin. Prefab'ı kaydet, sahnede 5 collectible ile test et — toplar kasada birikmeli. Bu hotfix bittikten sonra Faz 3.3 ✅. Kalan: 3.4 FailZone, 3.5a RotatingObstacle (pervane), 3.5b PendulumObstacle yeni script (çekiç sallanması), 3.6 MultiplierRamp (3 zone: x2/x3/x5). Hepsi `Assets/_Project/Prefabs/` altına kaydedilecek, hierarchy'den temizlenecek.
- [ ] Faz 4 — Data scriptleri v2 hazır (LevelData: gold/progress alanları). Level_01 prefab + LevelData + LevelDatabase asset oluşturulacak.
- [ ] Faz 5 — UI scriptleri v2 hazır (UIManager: progress bar + gold + reward delta, StarDisplay). Canvas kurulumu yapılacak.
- [ ] Faz 6 — Polish (CameraShake, particles, audio — opsiyonel)

> **Sonraki sohbet için başlangıç noktası:** Faz 3.3 Truck **hotfix mini-bloğu** (yukarıdaki açıklamada detay). Project'te Truck.prefab'ı çift tıkla, prefab edit modunda Cargo_Floor + 3 duvarın Y pozisyonlarını düşür ve her birine BoxCollider (Is Trigger off) ekle. Test: 5 collectible ile play, toplar kasada birikmeli. Tamamsa 3.3 ✅, sonra 3.4 FailZone (yere serili büyük BoxCollider isTrigger, Tag=FailZone), 3.5a RotatingObstacle pervane prefab'ı, 3.5b PendulumObstacle yeni script + çekiç prefab'ı, 3.6 MultiplierRamp (3 zone: x2/x3/x5). Tüm gameplay prefab'ları bitince Faz 4'te Level_01 prefab'ı içinde düzenlenecek + LevelData/LevelDatabase asset'leri oluşturulacak.

> **Vizyon notu (Faz 5'te bağlanacak gameflow):** A-şeması seçildi → Level boyunca 1-3 Gate + son Truck (yıldız+altın). UIManager'da otomatik geçiş: Win → 1.5s → next level; Lose → 1.5s → ReloadCurrent. Bu kod değişikliği Faz 5'te toplu yapılacak, şimdi UIManager'a dokunmuyoruz.

## Git & Repo
- **GitHub:** https://github.com/omerhalilislamoglu-lgtm/Picker-3d
- **Branch:** `main`
- Standart akış: `git add . && git commit -m "..." && git push`

## Teknik Notlar
- **Input:** Unity Input System paketi kurulu. `Pointer.current` API ile mouse+touch ortak kanal. `InputManager.HorizontalDelta` ekran genişliğine göre normalize edilmiş (`rawDx / Screen.width * lateralRange`) — küçük ve büyük ekranlarda tutarlı.
- **Render:** URP 17.4.0. Materyaller `Universal Render Pipeline/Lit` shader'ı kullanmalı.
- **Physics:** Default gravity (-9.81). Picker Rigidbody'si kinematic + FreezeRotation; topları `MovePosition` ile fiziksel olarak iter. Collectible'ların `Sphere Collider` kullanması performans için önemli (GDD).
- **Mobile:** `MobileBootstrap` sahnede olmalı: targetFrameRate=60, vSyncCount=0, NeverSleep, multiTouch off.
- **Singleton'lar:** GameManager, LevelManager, InputManager, GoldManager, LevelProgress — hepsi sahnede tek instance, `DontDestroyOnLoad` yok (tek sahne yeterli).
- **GoldManager:** PlayerPrefs key `picker3d.gold`. Add/TrySpend + OnGoldChanged event.
- **Multiplier akışı:** `MultiplierRamp` trigger'ı `Collectible.SetMultiplier(n)` ile topu işaretler; `Truck` topu yakalarken `WeightedScore += Multiplier` toplar. Gold = baseGold + WeightedScore * perBallGold.
- **TMP:** İlk Canvas oluşturulduğunda "Import TMP Essentials" diyalogu çıkar; kabul et.

## Convention'lar — Claude bunlara uy
- Kullanıcı Türkçe konuşur, sen de Türkçe cevapla.
- Editor talimatları **numaralı liste** halinde, kısa ve net olsun. "Hierarchy'de sağ tık → Create Empty → adı 'GameManager'" gibi.
- Inspector referans atama talimatı her zaman: "GameManager objesini seç → Inspector'da `levelManager` field'ına LevelManager objesini sürükle".
- Bir fazı bitirmeden bir sonrakine geçme. Faz sonu kabul kriteri tatmin edilince devam et.
- **Çalışma temposu — ÖNEMLİ:** Bir fazın alt-adımlarını (ör. Faz 3.3) tek mesajda 20 madde halinde verme. **Mikro-adımlar** halinde ilerle: bir mantıksal mini-blok (örn. "root oluştur + tag ata + collider ekle" → 3-5 madde) ver, kullanıcı uygulayıp "tamam" / "geçtim" / "sıradaki" deyince bir sonraki mini-bloğa geç. Hiyerarşi: Faz X → alt-adım X.Y → mini-blok 1, 2, 3 → her mini-blokta 3-5 numaralı talimat. Hata çıkarsa o mini-blokta dur, debug et, sonra ilerle.
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
