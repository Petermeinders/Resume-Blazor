// Declare firebaseConfig globally but don't initialize it here
let firebaseConfig = null;

// Declare firebaseInitPromise globally
let firebaseInitPromise = null;

// Declare a deferred promise that will be resolved when setFirebaseConfig is called and init is complete
let deferredInitPromiseResolve;
let deferredInitPromiseReject;
const deferredInitPromise = new Promise((resolve, reject) => {
    deferredInitPromiseResolve = resolve;
    deferredInitPromiseReject = reject;
});


// Function for Blazor to call to set the Firebase config
// This function will also trigger the Firebase initialization and define the global functions
window.setFirebaseConfig = (config) => {
    firebaseConfig = config; // Assign the config received from Blazor
    console.log("Firebase config set by Blazor.");

    // Now that config is set, proceed with Firebase initialization and define functions
    if (firebaseConfig) {
         // Promise that resolves when Firebase is ready and our custom functions are exposed
        firebaseInitPromise = new Promise((resolve, reject) => { // Assign to the global variable
            // No need for an interval here if we are inside setFirebaseConfig which is called after Blazor loads
            if (typeof firebase !== 'undefined') {
                try {
                    console.log("Firebase object defined. Initializing Firebase...");

                    const app = firebase.initializeApp(firebaseConfig);
                    const auth = firebase.auth(); // Initialize auth here
                    const db = firebase.firestore(); // Initialize db here

                    console.log("Firebase initialized successfully.");

                    // Expose functions to the global window object after initialization
                    window.signInWithGoogle = function() {
                         const provider = new firebase.auth.GoogleAuthProvider();
                         auth.signInWithPopup(provider)
                             .then((result) => {
                                 console.log("Signed in with Google:", result.user);
                             })
                             .catch((error) => {
                                 console.error("Google sign-in error:", error);
                             });
                     };

                     window.signOut = function() {
                         auth.signOut()
                             .then(() => {
                                 console.log("Signed out successfully.");
                             })
                             .catch((error) => {
                                 console.error("Sign-out error:", error);
                             });
                     };

                    resolve(); // Resolve the promise now that functions are exposed
                } catch (error) {
                    console.error("Error initializing Firebase:", error);
                    reject(error);
                }
            } else {
                console.error("Firebase object still not defined after script load.");
                reject(new Error("Firebase SDK not loaded."));
            }
        });

        // Add logging for promise resolution/rejection
        firebaseInitPromise // Use the global variable
            .then(() => {
                console.log("firebaseInitPromise resolved.");
                deferredInitPromiseResolve(); // Resolve the deferred promise
            })
            .catch((error) => {
                console.error("firebaseInitPromise rejected:", error);
                deferredInitPromiseReject(error); // Reject the deferred promise
            });

         // Store the DotNetObjectReference
        let blazorComponentRef = null;

        // Function to set the Blazor component reference (now inside setFirebaseConfig)
        window.setAuthStateChangeCallback = (componentRef) => {
            blazorComponentRef = componentRef;
            console.log("Blazor component reference set.");

            // **Immediately attach the auth state listener after the reference is set**
            if (typeof firebase !== 'undefined' && firebase.auth) {
                 firebase.auth().onAuthStateChanged((user) => {
                    console.log("Auth state changed:", user);
                    if (blazorComponentRef) {
                        const userInfo = user ? { displayName: user.displayName } : null;
                        blazorComponentRef.invokeMethodAsync('HandleAuthStateChanged', userInfo);
                    }
                });
            } else {
                console.error("Firebase or Auth not defined when setting auth state listener.");
            }

        };

        // Function to trigger an auth state check (will fire the listener) (now inside setFirebaseConfig)
        window.triggerAuthStateCheck = () => {
            if (typeof firebase !== 'undefined' && firebase.auth) {
                 // Calling getCurrentUser will trigger the onAuthStateChanged listener
                 firebase.auth().currentUser;
            } else {
                 console.error("Firebase or Auth not defined when triggering auth state check.");
            }
        };

        // Function to add a budget item to Firestore (now inside setFirebaseConfig)
        window.addBudgetItem = async (userId, item) => {
            try {
                const db = firebase.firestore(); // Get Firestore instance
                const userBudgetRef = db.collection('users').doc(userId).collection('budgetItems'); // Reference to user's budget items collection

                // **Create an object with explicit lowercase field names for Firestore**
                const firestoreItem = {
                    description: item.description, // **Access lowercase 'description' from the received item**
                    amount: item.amount // **Access lowercase 'amount' from the received item**
                    // Add other properties and map them similarly
                };

                await userBudgetRef.add(firestoreItem); // Add the item with standardized field names

                console.log("Budget item added to Firestore:", firestoreItem);
            } catch (error) {
                console.error("Error adding budget item to Firestore:", error);
                throw error; // Re-throw the error to be caught in Blazor
            }
        };

        // Function to get the current user's UID (now inside setFirebaseConfig)
        window.getCurrentUserId = () => {
            const auth = firebase.auth(); // Get Auth instance
            const user = auth.currentUser;
            return user ? user.uid : null;
        };

        // Function to load budget items from Firestore for a user (now inside setFirebaseConfig)
        window.loadBudgetItems = async (userId) => {
            try {
                const db = firebase.firestore(); // Get Firestore instance
                const userBudgetRef = db.collection('users').doc(userId).collection('budgetItems'); // Reference to user's budget items collection

                const snapshot = await userBudgetRef.get(); // Get the documents in the subcollection
                const items = [];
                snapshot.forEach(doc => {
                    const data = doc.data();
                    items.push({
                        Id: doc.id, // Use the document ID as the item ID
                        Description: data.description, // **Read lowercase 'description' field**
                        Amount: data.amount // **Read lowercase 'amount' field**
                        // Map other properties as needed
                    });
                });

                console.log("Budget items loaded from Firestore:", items);
                return items; // Return the array of items
            } catch (error) {
                console.error("Error loading budget items from Firestore:", error);
                throw error; // Re-throw the error
            }
        };


    } else {
        console.error("Firebase config not set by Blazor. Cannot initialize Firebase.");
        // Reject the deferred promise if config is not set
         deferredInitPromiseReject(new Error("Firebase config not set by Blazor."));
    }
};

// Define waitForFirebase globally immediately when the script is parsed
// It will return the deferred promise that is resolved when setFirebaseConfig is called and init is complete
window.waitForFirebase = () => {
   return deferredInitPromise; // Return the deferred promise
};
