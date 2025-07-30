// swift-tools-version: 6.2
// The swift-tools-version declares the minimum version of Swift required to build this package.

import PackageDescription

let package = Package(
    name: "Sorter",
    platforms: [.iOS(.v18), .macOS(.v15)],
    products: [
        // Sorter
        .library(
            name: "Sorter",
            targets: ["Sorter"]
        ),
    ],
    targets: [
        // Sort
        .target(
            name: "Sorter",
        ),
        .testTarget(
            name: "SorterTests",
            dependencies: ["Sorter"]
        ),
        ]
)
