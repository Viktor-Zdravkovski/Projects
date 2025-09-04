//Import the THREE.js library
import * as THREE from "https://cdn.skypack.dev/three@0.129.0/build/three.module.js";
// To allow for the camera to move around the scene
import { OrbitControls } from "https://cdn.skypack.dev/three@0.129.0/examples/jsm/controls/OrbitControls.js";
// To allow for importing the .gltf file
import { GLTFLoader } from "https://cdn.skypack.dev/three@0.129.0/examples/jsm/loaders/GLTFLoader.js";

const scene = new THREE.Scene();
const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.5, 1000);
camera.position.set(-17, -5, 7); // starting behind revolver
let barrelTip = null; // add this near your cameraTarget and zooming variables


// Renderer
const renderer = new THREE.WebGLRenderer({ alpha: true });
renderer.setSize(window.innerWidth, window.innerHeight);
document.getElementById("container3D").appendChild(renderer.domElement);

// Lights
const topLight = new THREE.DirectionalLight(0xffffff, 1);
topLight.position.set(500, 500, 500);
scene.add(topLight);

const ambientLight = new THREE.AmbientLight(0x333333, 2);
scene.add(ambientLight);

// GLTF Loader
const loader = new GLTFLoader();
let revolver, mixer;
const clock = new THREE.Clock();

// Camera target for barrel zoom
let cameraTarget = null;
let zooming = false;

// Parent group to lock revolver in place
const revolverGroup = new THREE.Object3D();
revolverGroup.position.set(-34, -10, -1); // revolver stays here
scene.add(revolverGroup);

// Load revolver
loader.load(
    "./models/revolver.glb",
    function (gltf) {
        revolver = gltf.scene;

        // SCALE
        revolver.scale.set(2, 2, 2);

        // add revolver to parent group
        revolverGroup.add(revolver);

        // Setup animation if exists
        if (gltf.animations && gltf.animations.length > 0) {
            mixer = new THREE.AnimationMixer(revolver);
            const action = mixer.clipAction(gltf.animations[0]);
            action.setLoop(THREE.LoopOnce);
            action.clampWhenFinished = true;

            // Delay animation by 1.5s
            setTimeout(() => {
                action.play();
            }, 1000);

            // When animation finishes → start camera zoom
            // mixer.addEventListener("finished", () => {
            //     // Set camera target in front of barrel
            //     cameraTarget = new THREE.Vector3(
            //         revolverGroup.position.x + 25,   // same x as revolver
            //         revolverGroup.position.y + 5.5, // slightly up if barrel is higher
            //         revolverGroup.position.z + 1.8    // in front of barrel, not inside
            //     );
            //     zooming = true;

            //     // Optional: load pizza after slight delay
            //     setTimeout(() => {
            //         loadPizza();
            //     }, 1000);
            // });
            mixer.addEventListener("finished", () => {
                // Camera target in front of the revolver barrel
                cameraTarget = new THREE.Vector3(
                    revolverGroup.position.x + 5,   // closer than +25, so revolver stays visible
                    revolverGroup.position.y + 1,   // slightly up
                    revolverGroup.position.z + 2    // slightly in front of the barrel
                );

                // Point for camera to look at (barrel tip)
                barrelTip = new THREE.Vector3(
                    revolverGroup.position.x,
                    revolverGroup.position.y + 0.5,
                    revolverGroup.position.z
                );

                zooming = true;

                // Optional: load pizza after slight delay
                setTimeout(() => {
                    loadPizza();
                }, 1000);
            });
        }
    },
    undefined,
    function (error) {
        console.error(error);
    }
);

// Load pizza
function loadPizza() {
    loader.load(
        "./models/pizza.glb",
        function (gltf) {
            const pizza = gltf.scene;
            pizza.scale.set(5, 5, 5);
            pizza.position.set(5, 0, 0); // to the right
            scene.add(pizza);

            pizza.traverse((child) => {
                if (child.isMesh) {
                    child.material.transparent = true;
                    child.material.opacity = 0;
                }
            });

            let opacity = 0;
            const fadeIn = () => {
                opacity += 0.02;
                pizza.traverse((child) => {
                    if (child.isMesh) child.material.opacity = Math.min(opacity, 1);
                });
                if (opacity < 1) requestAnimationFrame(fadeIn);
            };
            fadeIn();
        },
        undefined,
        (error) => console.error(error)
    );
}

// Animate loop
function animate() {
    requestAnimationFrame(animate);

    const delta = clock.getDelta();
    if (mixer) mixer.update(delta);

    // Smooth camera zoom after animation finishes
    if (zooming && cameraTarget) {
        camera.position.lerp(cameraTarget, 0.02);
        // camera.lookAt(revolverGroup.position);
        if (barrelTip) camera.lookAt(barrelTip);
    }

    renderer.render(scene, camera);
}

// Handle window resize
window.addEventListener("resize", () => {
    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();
    renderer.setSize(window.innerWidth, window.innerHeight);
});

animate();













// loader.load(
//     "./models/revolver.glb",
//     function (gltf) {
//         revolver = gltf.scene;

//         // SCALE (optional)
//         revolver.scale.set(2, 2, 2); // adjust size if needed

//         // POSITION on the far left
//         revolver.position.x = -17; // negative = left
//         revolver.position.y = -5; // adjust vertical alignment if needed
//         revolver.position.z = -1;  // keep it at center depth

//         scene.add(revolver);

//         // Setup animation (if revolver has any)
//         if (gltf.animations && gltf.animations.length > 0) {
//             mixer = new THREE.AnimationMixer(revolver);
//             const action = mixer.clipAction(gltf.animations[0]);
//             action.setLoop(THREE.LoopOnce);
//             action.clampWhenFinished = true;

//             // Delay animation by 1–2 seconds
//             setTimeout(() => {
//                 action.play();
//             }, 1500); // 1.5 seconds delay

//             // After animation finishes → load pizza
//             mixer.addEventListener("finished", () => {
//                 loadPizza();
//             });
//         } else {
//             console.warn("⚠️ No animations found in revolver.glb");
//         }
//     },
//     undefined,
//     function (error) {
//         console.error(error);
//     }
// );