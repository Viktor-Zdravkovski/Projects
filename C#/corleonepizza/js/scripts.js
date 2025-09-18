//Import the THREE.js library
import * as THREE from "https://cdn.skypack.dev/three@0.129.0/build/three.module.js";
// To allow for the camera to move around the scene
import { OrbitControls } from "https://cdn.skypack.dev/three@0.129.0/examples/jsm/controls/OrbitControls.js";
// To allow for importing the .gltf file
import { GLTFLoader } from "https://cdn.skypack.dev/three@0.129.0/examples/jsm/loaders/GLTFLoader.js";



const scene = new THREE.Scene();
// const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.5, 1000);
const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);

camera.position.set(15, 5, 20); // starting behind revolver
let barrelTip = null; // add this near your cameraTarget and zooming variables

const renderer = new THREE.WebGLRenderer({ alpha: true });
renderer.setSize(window.innerWidth, window.innerHeight);
document.getElementById("container3D").appendChild(renderer.domElement);

// Lights
const topLight = new THREE.DirectionalLight(0xffffff, 1);
topLight.position.set(1000, 800, 500);
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
revolverGroup.position.set(-10, 0, 0); // revolver stays here
scene.add(revolverGroup);

// Load revolver
loader.load(
    "./models/revolvernew.glb",
    function (gltf) {
        revolver = gltf.scene;

        // SCALE
        revolver.scale.set(8, 8, 8);

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
                    revolverGroup.position.x + 35,   // closer than +25, so revolver stays visible
                    revolverGroup.position.y + 5,   // slightly up
                    revolverGroup.position.z + 7.50    // slightly in front of the barrel
                );

                // Point for camera to look at (barrel tip)
                barrelTip = new THREE.Vector3(
                    revolverGroup.position.x + 30,
                    revolverGroup.position.y + 5,
                    revolverGroup.position.z + 7.50
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
                revolverGroup.position.x + 35,
                revolverGroup.position.y + 5.93,
                revolverGroup.position.z + 7.555
            );

            // Scale pizza to fit barrel
            pizza.scale.set(0.4, 0.4, 0.4);

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

// // New function: smoothly zoom camera into pizza
function zoomToPizza(pizza) {
    // Get pizza world position
    const pizzaPos = pizza.getWorldPosition(new THREE.Vector3());

    // Compute pizza center
    const box = new THREE.Box3().setFromObject(pizza);
    const center = box.getCenter(new THREE.Vector3());

    // First target: in front of barrel (already there)
    const pizzaTarget = new THREE.Vector3(
        center.x + 999,   // not huge like +990, just in front
        center.y + 1,
        center.z
    );

    const pizzaLookAt = center;
    const zoomSpeed = 0.02;

    // Fade revolver out
    revolver.traverse((child) => {
        if (child.isMesh) child.material.transparent = true;
    });

    let fadeAmount = 0;

    const zoomStep = () => {
        camera.position.lerp(pizzaTarget, zoomSpeed);
        camera.lookAt(pizzaLookAt);

        // Gradually fade revolver
        fadeAmount += 0.01;
        revolver.traverse((child) => {
            if (child.isMesh) child.material.opacity = Math.max(1 - fadeAmount, 0);
        });

        if (fadeAmount < 1) {
            requestAnimationFrame(zoomStep);
        } else {
            // When revolver is gone → start final zoom into pizza
            finalZoomIn(pizza);
        }
    };

    zoomStep();
}

// Final zoom-in once revolver is gone
function finalZoomIn(pizza) {
    pizza.scale.set(1.5, 1.5, 1.5);
    const box = new THREE.Box3().setFromObject(pizza);
    const center = box.getCenter(new THREE.Vector3());

    const closeTarget = new THREE.Vector3(
        center.x + 1,
        center.y - 3,
        center.z + 4
    );

    const zoomSpeed = 0.02;


    // Inside finalZoomIn() after camera reaches closeTarget
    const zoomCloser = () => {
        camera.position.lerp(closeTarget, zoomSpeed);
        camera.lookAt(center);

        if (camera.position.distanceTo(closeTarget) > 0.05) {
            requestAnimationFrame(zoomCloser);
        }


    };
    const rightSide = document.querySelector(".right-side");
    const additional = document.querySelector(".additional-section");
    const pizzajourney = document.querySelector(".pizza-journey");

    // When final zoom finishes
    rightSide.classList.add('show');
    additional.classList.add('show');
    pizzajourney.classList.add('show');
    document.body.classList.remove("hidden");


    zoomCloser();
}

// NAV BAR FUNC TO SHOW WHEN SCROLLED
let navbarShown = false;

window.addEventListener("scroll", function () {
    if (!navbarShown && window.scrollY > 10) {
        document.querySelector(".navbar").classList.add("visible");
        navbarShown = true;
    }
});
// ================================================================

const faqQuestions = document.querySelectorAll(".faq-question");

faqQuestions.forEach((question) => {
    question.addEventListener("click", () => {
        const answer = question.nextElementSibling;

        // Close other answers
        document.querySelectorAll(".faq-answer").forEach((ans) => {
            if (ans !== answer) {
                ans.style.maxHeight = null;
            }
        });

        // Toggle clicked one
        if (answer.style.maxHeight) {
            answer.style.maxHeight = null;
        } else {
            answer.style.maxHeight = answer.scrollHeight + "px";
        }
    });
});



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
