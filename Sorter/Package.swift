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
        
        // SortDescriptor
        .library(
            name: "SortDescriptor",
            targets: ["SortDescriptor"]
        ),
        
        // SortValues
        .library(
            name: "SortValues",
            targets: ["SortValues"]
        )
    ],
    dependencies: [
        .package(url: "https://github.com/apple/swift-collections.git", branch: "main")
    ],
    targets: [
        // Sort
        .target(
            name: "Sorter",
            dependencies: [
                "SortDescriptor",
                "SortValues",
                .product(name: "Collections", package: "swift-collections")
            ]
        ),
        .testTarget(
            name: "SorterTests",
            dependencies: ["Sorter", "SortValues"]
        ),
        
        
        // SortDescriptor
        .target(
            name: "SortDescriptor",
            dependencies: [
                "SortValues",
                .product(name: "Collections", package: "swift-collections")
            ]
        ),
        .testTarget(
            name: "SortDescriptorTests",
            dependencies: ["SortDescriptor", "SortValues"]
        ),
        
        // SortValues
        .target(
            name: "SortValues"
        )
        
        ]
)
