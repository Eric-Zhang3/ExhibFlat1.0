/*
 * by zhangxinxu(.com) on 2013-12-11
 * ÐèÒª json2  sizzle
 * under MIT license
*/

(function(window, document, undefined) {
    /*
	 * Object ES5 extend
	*/
    if (!Object.create) {
        Object.create = function(o) {
            if (arguments.length > 1) {
                throw new Error('Object.create implementation only accepts the first parameter.');
            }

            function F() {}

            F.prototype = o;
            return new F();
        };
    }

    if (!Object.keys) {
        Object.keys = function(o) {
            if (o !== Object(o)) {
                throw new TypeError('Object.keys called on a non-object');
            }
            var k = [], p;
            for (p in o) {
                if (Object.prototype.hasOwnProperty.call(o, p)) {
                    k.push(p);
                }
            }
            return k;
        };
    }

    /*
	 * Date ES5 extend
	*/
    if (!Date.now) {
        Date.now = function now() {
            return (new Date).valueOf();
        };
    }


    /*
	 * JSON ES5 extend
	 * Now use json2.js at the bottom of this file
	*/
    /*if (!window.JSON) {
		window.JSON = {
			parse: function (sJSON) { return eval("(" + sJSON + ")"); },
			stringify: function (vContent) {
				if (vContent instanceof Object) {
					var sOutput = "";
					if (vContent.constructor === Array) {
			 			for (var nId = 0; nId < vContent.length; sOutput += this.stringify(vContent[nId]) + ",", nId++);
			  				return "[" + sOutput.substr(0, sOutput.length - 1) + "]";
					}
					if (vContent.toString !== Object.prototype.toString) { 
						return "\"" + vContent.toString().replace(/"/g, "\\$&") + "\"";
					}
					for (var sProp in vContent) {
						sOutput += "\"" + sProp.replace(/"/g, "\\$&") + "\":" + this.stringify(vContent[sProp]) + ",";
					}
					return "{" + sOutput.substr(0, sOutput.length - 1) + "}";
				}
		  		return typeof vContent === "string" ? "\"" + vContent.replace(/"/g, "\\$&") + "\"" : String(vContent);
			}
	  	};
	}*/

    /*
	 * Function ES5 extend
	*/
    if (!Function.prototype.bind) {
        Function.prototype.bind = function(oThis) {
            if (typeof this !== "function") {
                // closest thing possible to the ECMAScript 5 internal IsCallable function
                throw new TypeError("Function.prototype.bind - what is trying to be bound is not callable");
            }

            var aArgs = Array.prototype.slice.call(arguments, 1),
                fToBind = this,
                fNOP = function() {},
                fBound = function() {
                    return fToBind.apply(this instanceof fNOP && oThis
                        ? this
                        : oThis || window,
                        aArgs.concat(Array.prototype.slice.call(arguments)));
                };

            fNOP.prototype = this.prototype;
            fBound.prototype = new fNOP();

            return fBound;
        };
    }

    /*
	 * String ES5 extend
	*/
    if (!String.prototype.trim) {
        String.prototype.trim = function() {
            return this.replace(/^\s+|\s+$/g, '');
        };
    }

    /*
	 * Array ES5 extend
	*/
    if (!Array.isArray) {
        Array.isArray = function(vArg) {
            return Object.prototype.toString.call(vArg) === "[object Array]";
        };
    }

    if (typeof Array.prototype.forEach != "function") {
        Array.prototype.forEach = function(fn, scope) {
            var i, len;
            for (i = 0, len = this.length; i < len; ++i) {
                if (i in this) {
                    fn.call(scope, this[i], i, this);
                }
            }
        };
    }

    if (typeof Array.prototype.map != "function") {
        Array.prototype.map = function(fn, context) {
            var arr = [];
            if (typeof fn === "function") {
                for (var k = 0, length = this.length; k < length; k++) {
                    arr.push(fn.call(context, this[k], k, this));
                }
            }
            return arr;
        };
    }

    if (typeof Array.prototype.filter != "function") {
        Array.prototype.filter = function(fn, context) {
            var arr = [];
            if (typeof fn === "function") {
                for (var k = 0, length = this.length; k < length; k++) {
                    fn.call(context, this[k], k, this) && arr.push(this[k]);
                }
            }
            return arr;
        };
    }

    if (typeof Array.prototype.some != "function") {
        Array.prototype.some = function(fn, context) {
            var passed = false;
            if (typeof fn === "function") {
                for (var k = 0, length = this.length; k < length; k++) {
                    if (passed === true) break;
                    passed = !!fn.call(context, this[k], k, this);
                }
            }
            return passed;
        };
    }

    if (typeof Array.prototype.every != "function") {
        Array.prototype.every = function(fn, context) {
            var passed = true;
            if (typeof fn === "function") {
                for (var k = 0, length = this.length; k < length; k++) {
                    if (passed === false) break;
                    passed = !!fn.call(context, this[k], k, this);
                }
            }
            return passed;
        };
    }

    if (typeof Array.prototype.indexOf != "function") {
        Array.prototype.indexOf = function(searchElement, fromIndex) {
            var index = -1;
            fromIndex = fromIndex * 1 || 0;

            for (var k = 0, length = this.length; k < length; k++) {
                if (k >= fromIndex && this[k] === searchElement) {
                    index = k;
                    break;
                }
            }
            return index;
        };
    }

    if (typeof Array.prototype.lastIndexOf != "function") {
        Array.prototype.lastIndexOf = function(searchElement, fromIndex) {
            var index = -1, length = this.length;
            fromIndex = fromIndex * 1 || length - 1;

            for (var k = length - 1; k > -1; k -= 1) {
                if (k <= fromIndex && this[k] === searchElement) {
                    index = k;
                    break;
                }
            }
            return index;
        };
    }

    if (typeof Array.prototype.reduce != "function") {
        Array.prototype.reduce = function(callback, initialValue) {
            var previous = initialValue, k = 0, length = this.length;
            if (typeof initialValue === "undefined") {
                previous = this[0];
                k = 1;
            }

            if (typeof callback === "function") {
                for (k; k < length; k++) {
                    this.hasOwnProperty(k) && (previous = callback(previous, this[k], k, this));
                }
            }
            return previous;
        };
    }

    if (typeof Array.prototype.reduceRight != "function") {
        Array.prototype.reduceRight = function(callback, initialValue) {
            var length = this.length, k = length - 1, previous = initialValue;
            if (typeof initialValue === "undefined") {
                previous = this[length - 1];
                k--;
            }
            if (typeof callback === "function") {
                for (k; k > -1; k -= 1) {
                    this.hasOwnProperty(k) && (previous = callback(previous, this[k], k, this));
                }
            }
            return previous;
        };
    }

    /**
	 * dom method that extend
	*/
    var oDomExtend = {
        // selector realtive
        querySelector: function(selector) {
            return oDomExtend.querySelectorAll.call(this, selector)[0] || null;
        },
        querySelectorAll: function(selector) {
            return fDomExtend(Sizzle(selector, this));
        },
        getElementsByClassName: function(classNames) {
            return this.querySelectorAll("." + classNames.trim().replace(/\s+/, "."));
        },
        // addEventListener
        addEventListener: function(eventType, funcHandle, useCapture) {
            var element = this, eventStoreType = '';
            if (eventType == "input") {
                eventType = "propertychange";
            }
            if (typeof funcHandle != "function") return;
            // some compatibility deal
            var eventHandle = function(event) {
                event = event || window.event || {};

                if (!event.target) event.target = event.srcElement;
                if (!event.preventDefault)
                    event.preventDefault = function() {
                        event.returnValue = false;
                    };

                if (eventType == "propertychange") {
                    if (event.propertyName !== "value" || element.r_oldvalue === element.value) return;
                    element.r_oldvalue = element.value;
                }
                return funcHandle.call(element, event || {});
            };
            eventHandle.initFuncHandle = funcHandle;

            // event bind
            element.attachEvent("on" + eventType, eventHandle);

            // event store
            if (element["event" + eventType]) {
                element["event" + eventType].push(eventHandle);
            } else {
                element["event" + eventType] = [eventHandle];
            }
        },
        dispatchEvent: function(event) {
            var eventType = event && event.type;
            if (eventType && this["event" + eventType]) {
                event.target = this;
                this["event" + eventType].forEach(function(eventHandle) {
                    event.timeStamp = Date.now();
                    eventHandle.call(this, event);
                }.bind(this));
            }
        },
        removeEventListener: function(eventType, funcHandle, useCapture) {
            var arrEventStore = this["event" + eventType];
            if (Array.isArray(arrEventStore)) {
                this["event" + eventType] = arrEventStore.filter(function(eventHandle) {
                    if (eventHandle.initFuncHandle === funcHandle) {
                        this.detachEvent("on" + eventType, eventHandle);
                        return false;
                    }
                    return true;
                }.bind(this));
            }
        }

    };

    var fDomExtend = function(collection) {
        // collection extend some dom method
        collection.forEach(function(element, index) {
            for (var key in oDomExtend) {
                element[key] = oDomExtend[key].bind(element);
            }
        });
        return collection;
    };

    /* 
	 * document.querySelector, document.querySelectorAll
	*/
    document.querySelector = function(selector) {
        return document.querySelectorAll(selector)[0] || null;
    };
    document.querySelectorAll = function(selector) {
        var collection = Sizzle(selector);
        return fDomExtend(collection);
    };
    /* 
	 * getElementsByClassName
	*/
    if (!document.getElementsByClassName) {
        document.getElementsByClassName = function(classNames) {
            return oDomExtend.getElementsByClassName.call(document, classNames);
        };
    }
    /* 
	 * addEventListener
	 * include event of "input"
	*/
    if (typeof document.addEventListener == "undefined") {
        [window, document].forEach(function(global) {
            global.addEventListener = function(eventType, funcHandle, useCapture) {
                oDomExtend.addEventListener.call(global, eventType, funcHandle, useCapture);
            };
            global.dispatchEvent = function(event) {
                oDomExtend.dispatchEvent.call(global, event);
            };
            global.removeEventListener = function(eventType, funcHandle, useCapture) {
                oDomExtend.removeEventListener.call(global, eventType, funcHandle, useCapture);
            };
        });
    }
    if (!document.createEvent) {
        document.createEvent = function(type) {
            var event = {};
            switch (type) {
            case "Event":
            case "Events":
            case "HTMLEvents":
            {
                event = {
                    initEvent: function(eventType, canBubble, cancelable) {
                        event.type = eventType;
                        event.canBubble = canBubble || false;
                        event.cancelable = cancelable || false;
                        delete(event.initEvent);
                    },
                    bubbles: false,
                    cancelBubble: false,
                    cancelable: false,
                    clipboardData: undefined,
                    currentTarget: null,
                    defaultPrevented: false,
                    eventPhase: 0,
                    returnValue: true,
                    srcElement: null,
                    target: null,
                    timeStamp: Date.now(),
                    type: ""
                };

                break;
            }
            case "MouseEvents":
            {
                event = {
                    initMouseEvent: function(eventType, canBubble, cancelable, view,
                        detail, screenX, screenY, clientX, clientY,
                        ctrlKey, altKey, shiftKey, metaKey,
                        button, relatedTarget
                    ) {
                        event.type = eventType;
                        event.canBubble = canBubble || false;
                        event.cancelable = cancelable || false;
                        event.view = view || null;
                        event.screenX = screenX || 0;
                        event.screenY = screenY || 0;
                        event.clientX = clientX || 0;
                        event.clientY = clientY || 0;
                        event.ctrlKey = ctrlKey || false;
                        event.altKey = altKey || false;
                        event.shiftKey = shiftKey || false;
                        event.metaKey = metaKey || false;
                        event.button = button || 0;
                        event.relatedTarget = relatedTarget || null;
                        delete(event.initMouseEvent);
                    },
                    altKey: false,
                    bubbles: false,
                    button: 0,
                    cancelBubble: false,
                    cancelable: false,
                    charCode: 0,
                    clientX: 0,
                    clientY: 0,
                    clipboardData: undefined,
                    ctrlKey: false,
                    currentTarget: null,
                    dataTransfer: null,
                    defaultPrevented: false,
                    detail: 0,
                    eventPhase: 0,
                    fromElement: null,
                    keyCode: 0,
                    layerX: 0,
                    layerY: 0,
                    metaKey: false,
                    offsetX: 0,
                    offsetY: 0,
                    pageX: 0,
                    pageY: 0,
                    relatedTarget: null,
                    returnValue: true,
                    screenX: 0,
                    screenY: 0,
                    shiftKey: false,
                    srcElement: null,
                    target: null,
                    timeStamp: Date.now(),
                    toElement: null,
                    type: "",
                    view: null,
                    webkitMovementX: 0,
                    webkitMovementY: 0,
                    which: 0,
                    x: 0,
                    y: 0
                };

                break;
            }
            case "UIEvents":
            {
                event = {
                    initUIEvent: function(eventType, canBubble, cancelable, view, detail) {
                        event.type = eventType;
                        event.canBubble = canBubble || false;
                        event.cancelable = cancelable || false;
                        event.view = view || null;
                        event.detail = detail || 0;
                        delete(event.initUIEvent);
                    },
                    bubbles: false,
                    cancelBubble: false,
                    cancelable: false,
                    charCode: 0,
                    clipboardData: undefined,
                    currentTarget: null,
                    defaultPrevented: false,
                    detail: 0,
                    eventPhase: 0,
                    keyCode: 0,
                    layerX: 0,
                    layerY: 0,
                    pageX: 0,
                    pageY: 0,
                    returnValue: true,
                    srcElement: null,
                    target: null,
                    timeStamp: Date.now(),
                    type: "",
                    view: null,
                    which: 0
                };
                break;
            }
            default:
            {
                throw new TypeError("NotSupportedError: The implementation did not support the requested type of object or operation.");
            }
            }
            return event;
        };
    }

    /**
	 * onhashchange
	*/
    // exit if the browser implements that event
    if (!("addEventListener" in document.createElement("div"))) {
        var location = window.location,
            oldURL = location.href,
            oldHash = location.hash;

        // check the location hash on a 100ms interval
        setInterval(function() {
            var newURL = location.href,
                newHash = location.hash;

            // if the hash has changed and a handler has been bound...
            if (newHash != oldHash && typeof window.onhashchange === "function") {
                // execute the handler
                window.onhashchange({
                    type: "hashchange",
                    oldURL: oldURL,
                    newURL: newURL
                });

                oldURL = newURL;
                oldHash = newHash;
            }
        }, 100);
    }

    /**
	 * getComputedStyle
	*/
    if (typeof window.getComputedStyle !== "function") {
        window.getComputedStyle = function(el, pseudo) {
            var oStyle = {};
            var oCurrentStyle = el.currentStyle || {};
            for (var key in oCurrentStyle) {
                oStyle[key] = oCurrentStyle[key];
            }

            oStyle.styleFloat = oStyle.cssFloat;

            oStyle.getPropertyValue = function(prop) {
                // return oCurrentStyle.getAttribute(prop) || null;  // IE6 do not support "key-key" but "keyKey"
                var re = /(\-([a-z]){1})/g;
                if (prop == 'float') prop = 'styleFloat';
                if (re.test(prop)) {
                    prop = prop.replace(re, function() {
                        return arguments[2].toUpperCase();
                    });
                }
                return el.currentStyle[prop] ? el.currentStyle[prop] : null;
            }
            return oStyle;
        }
    }

})(window, document);