//Import the THREE.js library
import * as THREE from "https://cdn.skypack.dev/three@0.129.0/build/three.module.js";
// To allow for the camera to move around the scene
import { OrbitControls } from "https://cdn.skypack.dev/three@0.129.0/examples/jsm/controls/OrbitControls.js";
// To allow for importing the .gltf file
import { GLTFLoader } from "https://cdn.skypack.dev/three@0.129.0/examples/jsm/loaders/GLTFLoader.js";

const scene = new THREE.Scene();
// const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.5, 1000);
const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);

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

            mixer.addEventListener("finished", () => {
                // Camera target in front of the revolver barrel
                cameraTarget = new THREE.Vector3(
                    revolverGroup.position.x + 25,   // closer than +25, so revolver stays visible
                    revolverGroup.position.y + 5.6,   // slightly up
                    revolverGroup.position.z + 1.95    // slightly in front of the barrel
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

            // Attach pizza to revolverGroup so it stays with revolver
            revolverGroup.add(pizza);

            // Position pizza inside the barrel (keep your existing values)
            pizza.position.set(
                revolverGroup.position.x + 50,
                revolverGroup.position.y + 15.65,
                revolverGroup.position.z + 2.85
            );

            // Scale pizza to fit barrel
            pizza.scale.set(0.2, 0.2, 0.2);

            // Rotate pizza so top faces camera
            pizza.rotation.set(-Math.PI / 1, 0, 4.60);

            // Make pizza transparent initially
            pizza.traverse((child) => {
                if (child.isMesh) {
                    child.material.transparent = true;
                    child.material.opacity = 0;
                }
            });

            // Fade in pizza smoothly
            let opacity = 0;
            const fadeIn = () => {
                opacity += 0.05;
                pizza.traverse((child) => {
                    if (child.isMesh) child.material.opacity = Math.min(opacity, 1);
                });
                if (opacity < 1) {
                    requestAnimationFrame(fadeIn);
                } else {
                    // When fade-in finishes, start zoom into pizza
                    zoomToPizza(pizza);
                }
            };
            fadeIn();
        },
        undefined,
        (error) => console.error(error)
    );
}

// New function: smoothly zoom camera into pizza
function zoomToPizza(pizza) {
    // Get pizza world position
    const pizzaPos = pizza.getWorldPosition(new THREE.Vector3());

    // Compute pizza center bounding box for precise alignment
    const box = new THREE.Box3().setFromObject(pizza);
    const center = box.getCenter(new THREE.Vector3());

    // Camera target: directly in front of pizza, slightly closer along X-axis
    const pizzaTarget = new THREE.Vector3(
        center.x - 20,  // forward into barrel
        center.y + 1,      // exact vertical center
        center.z       // exact depth center
    );

    // Camera should look at pizza center
    const pizzaLookAt = center;

    const zoomSpeed = 0.008;

    // Make revolver fade out
    revolver.traverse((child) => {
        if (child.isMesh) child.material.transparent = true;
    });

    let fadeAmount = 0;

    const zoomStep = () => {
        camera.position.lerp(pizzaTarget, zoomSpeed);
        camera.lookAt(pizzaLookAt);

        // Gradually fade revolver
        fadeAmount += 0.005;
        revolver.traverse((child) => {
            if (child.isMesh) child.material.opacity = Math.max(1 - fadeAmount, 0);
        });

        if (camera.position.distanceTo(pizzaTarget) > 0.05) {
            requestAnimationFrame(zoomStep);
        } else {
            revolver.traverse((child) => {
                if (child.isMesh) child.material.opacity = 0;
            });
        }
    };

    zoomStep();
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